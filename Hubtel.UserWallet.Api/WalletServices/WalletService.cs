using Hubtel.UserWallet.Api.Data;
using Hubtel.UserWallet.Api.ReturnTypes;
using Hubtel.UserWallet.Api.ReusableMethods;
using Hubtel.UserWallet.Api.WalletModels;
using Hubtel.UserWallet.Api.WalletModels.Interfaces;
using Hubtel.UserWallet.Api.WalletModels.WalletEnums;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;

namespace Hubtel.UserWallet.Api.WalletServices
{
    public class WalletService
        (DataContext context, 
        IHelperMethods helper,
        IServiceResponseFactory<IWalletServiceResponse> responseFactory) 
        : IWalletService
    {
        IServiceResponseFactory<IWalletServiceResponse> _responseFactory = responseFactory;
        DataContext _context = context;
        IHelperMethods _helper = helper;

        public async Task<IWalletDataModel> GetWallet(int id)
        {
            var wallet = await _helper.GetById(id, _context);
            return wallet!;
        }

        public async Task<IWalletDataModel> GetWallet(string name)
        {
            var wallet = await _helper.GetByName(name, _context);
            return wallet!;
        }
        public async Task<IWalletServiceResponse> RemoveWallet(int id)
        {
            var wallet = await _helper.GetWallet(id, _context);
            if (wallet == null)
                return _responseFactory.GetResponse(false, "wallet was not found");
            
            _context.Wallets.Remove(wallet);
            await _context.SaveChangesAsync();
            return _responseFactory.GetResponse(true, "wallet was deleted");
        }

        public async Task<IEnumerable<WalletDataModel>> GetAllAsync()
        {
            return await _context.Wallets.ToListAsync();
        }

        public async Task<IWalletServiceResponse> CreateAsync(IWalletPostModel model)
        {
            var walletSchemeEnumLength = Enum.GetNames(typeof(WalletScheme)).Length;
            var walletTypeLength = Enum.GetNames(typeof(WalletType)).Length;
            
            if (model.WalletType < 0 || (int)model.WalletType >= walletTypeLength )
                return _responseFactory.GetResponse(false, $"index {model.WalletType} was out of range\nuse \"0\" for \"Momo\" \r\n\r\n\"1\" for \"Card\" ");

            if (model.AccountScheme < 0 || (int)model.AccountScheme > walletSchemeEnumLength)
                return _responseFactory.GetResponse(false, $"index {model.AccountScheme} was out of range\nuse 0 for Visa,\r\n    \r\n1 for mastercard\r\n    \r\n2 for mtn\r\n   \r\n3 for vodafone\r\n  \r\n4 for airteltigo ");

            if (!_helper.TypeAndSchemeMatch(model))
                return _responseFactory.GetResponse(false, $"Value '{model.AccountScheme}' does not match with '{model.WalletType}'");

            if (!await _helper.CanCreateMoreWalletsAsync(_context))
                return _responseFactory.GetResponse(false, "Cannot create more than four wallets");

            if (await _helper.WalletExists(model, _context))
                return _responseFactory.GetResponse(false, $"Wallet with account number '{model.AccountNumber}' already exists ");
            

            if (model is not null)
            {
                var walletModel = new WalletDataModel
                {
                    CreatedAt = DateTime.Now,
                    Owner = model.Owner,
                    Name = model.Name,
                    Type = Enum.GetName(typeof(WalletType), model.WalletType)!,
                    AccountNumber = _helper.GenerateAccountNumber(model.AccountNumber, model.WalletType),
                    AccountScheme = Enum.GetName(typeof(WalletScheme), model.AccountScheme)!

                };
                await _context.Wallets.AddAsync(walletModel);
                await _context.SaveChangesAsync();
            }
            return _responseFactory.GetResponse(true, "Wallet created");
        }

    }
}
