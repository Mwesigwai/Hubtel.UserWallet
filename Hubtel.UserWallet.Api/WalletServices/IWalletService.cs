using Hubtel.UserWallet.Api.ReturnTypes;
using Hubtel.UserWallet.Api.WalletModels;
using Hubtel.UserWallet.Api.WalletModels.Interfaces;

namespace Hubtel.UserWallet.Api.WalletServices
{
    public interface IWalletService
    {
        Task<IWalletServiceResponse> CreateAsync(IWalletPostModel model);
        Task<IEnumerable<WalletDataModel>> GetAllAsync();
        Task<IWalletDataModel> GetWallet(int id);
        Task<IWalletDataModel> GetWallet(string name);
        Task<IWalletServiceResponse> RemoveWallet(int id);
    }
}