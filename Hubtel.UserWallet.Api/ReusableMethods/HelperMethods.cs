using Hubtel.UserWallet.Api.Data;
using Hubtel.UserWallet.Api.WalletModels;
using Hubtel.UserWallet.Api.WalletModels.WalletEnums;
using Microsoft.EntityFrameworkCore;

namespace Hubtel.UserWallet.Api.ReusableMethods
{
    public class HelperMethods : IHelperMethods
    {
        public string GenerateAccountNumber(string accountNum, WalletType type)
        {
            return
                type is WalletType.Card ? accountNum[..6]
                : accountNum;
        }

        public async Task<bool> WalletExists(IWalletPostModel model, DataContext context)
        {
            if (await context.Wallets.AnyAsync(w => w.Owner == model.Owner && w.Name == model.Name))
                return true;
            return false;
        }

        public bool TypeAndSchemeMatch(IWalletPostModel model)
        {
            if (model.WalletType is WalletType.Momo)
            {
                return
                    model.AccountScheme is WalletScheme.aireteltigo ||
                    model.AccountScheme is WalletScheme.mtn ||
                    model.AccountScheme is WalletScheme.vodafone;
            }
            else
            {
                return
                    model.AccountScheme is WalletScheme.visa ||
                    model.AccountScheme is WalletScheme.mastercard;
            }
        }


        public async Task<bool> CanCreateMoreWalletsAsync(DataContext context)
        {
            return
                await context.Wallets.CountAsync() < 4;
        }

        public async Task<WalletDataModel> GetWallet(string walletName,DataContext context)
        {
            var wallet = await
                         context.Wallets
                         .FirstOrDefaultAsync(w => w.Name == walletName);
           
            return wallet!;
        }

        public async Task<WalletDataModel> GetById(int id, DataContext context)
        {
            var wallet = await context
                               .Wallets
                               .FirstOrDefaultAsync(w => w.ID == id);
            return wallet!;
        }

        public async Task<WalletDataModel> GetByName(string name, DataContext context)
        {
            var wallet = await context.Wallets.FirstOrDefaultAsync(w => w.Name == name);
            return wallet!;
        }
    }
}
