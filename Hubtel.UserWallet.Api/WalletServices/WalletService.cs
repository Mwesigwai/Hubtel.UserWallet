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
    public class WalletService(DataContext context, IHelperMethods helper) : IWalletService
    {
        DataContext _context = context;
        IHelperMethods _helper = helper;

        public async Task<WalletDataModel> GetWallet(int id)
        {
            var wallet = await _helper.GetById(id, _context);
            return wallet!;
        }

        public async Task<WalletDataModel> GetWallet(string name)
        {
            var wallet = await _helper.GetByName(name, _context);
            return wallet!;
        }
        public async Task<WalletServiceResponse> RemoveWallet(int id)
        {
            var wallet = await _helper.GetWallet(id, _context);
            if (wallet == null)
            {
                return new WalletServiceResponse
                {
                    OperationSuccessful = false,
                    Message = "wallet was not found"
                };
            }
            _context.Wallets.Remove(wallet);
            await _context.SaveChangesAsync();
            return new WalletServiceResponse
            {
                OperationSuccessful = true,
                Message = "wallet was deleted"
            };
        }

        public async Task<IEnumerable<WalletDataModel>> GetAllAsync()
        {
            return await _context.Wallets.ToListAsync();
        }

        public async Task<WalletServiceResponse> CreateAsync(IWalletPostModel model)
        {
            if (!_helper.TypeAndSchemeMatch(model))
            {
                return new()
                {
                    Message = $"Value '{model.AccountScheme}' does not match with '{model.WalletType}'",
                    OperationSuccessful = false
                };
            }
            if (!await _helper.CanCreateMoreWalletsAsync(_context))
            {
                return new()
                {
                    Message = "Cannot create more than four wallets",
                    OperationSuccessful = false
                };
            }
            if (await _helper.WalletExists(model, _context))
            {
                return new()
                {
                    Message = $"Wallet with account number '{model.AccountNumber}' already exists ",
                    OperationSuccessful = false
                };
            }
            

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
            return new()
            {
                Message = "Wallet created",
                OperationSuccessful = true
            };

        }


    }
}
