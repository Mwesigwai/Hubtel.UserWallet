using Hubtel.UserWallet.Api.Data;
using Hubtel.UserWallet.Api.WalletModels;
using Hubtel.UserWallet.Api.WalletModels.Interfaces;
using Hubtel.UserWallet.Api.WalletModels.WalletEnums;
using Microsoft.EntityFrameworkCore;

namespace Hubtel.UserWallet.Api.ReusableMethods
{
    public interface IHelperMethods
    {
        Task<bool> CanCreateMoreWalletsAsync(DataContext context);
        string GenerateAccountNumber(string accountNum, WalletType type);
        Task<WalletDataModel> GetById(int id,DataContext context);
        Task<WalletDataModel> GetByName(string name, DataContext context);
        Task<WalletDataModel> GetWallet(int id,DataContext context);
        bool TypeAndSchemeMatch(IWalletPostModel model);
        Task<bool> WalletExists(IWalletPostModel model, DataContext context);
    }
}