using Hubtel.UserWallet.Api.ReturnTypes;
using Hubtel.UserWallet.Api.WalletModels;
using Hubtel.UserWallet.Api.WalletModels.Interfaces;

namespace Hubtel.UserWallet.Api.WalletServices
{
    public interface IWalletService
    {
        Task<WalletServiceResponse> CreateAsync(IWalletPostModel model);
        Task<IEnumerable<WalletDataModel>> GetAllAsync();
        Task<WalletDataModel> GetWallet(int id);
        Task<WalletDataModel> GetWallet(string name);
        Task<WalletServiceResponse> RemoveWallet(int id);
    }
}