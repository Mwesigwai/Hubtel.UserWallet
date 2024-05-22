using Hubtel.UserWallet.Api.ReturnTypes;
using Hubtel.UserWallet.Api.WalletModels;

namespace Hubtel.UserWallet.Api.Data
{
    public interface IWalletService
    {
        Task<WalletServiceResponse> CreateAsync(IWalletPostModel model);
        Task<IEnumerable<WalletDataModel>> GetAllAsync();
        Task<WalletDataModel> GetItem(int id);
        Task<WalletDataModel> GetItem(string name);
        Task<WalletServiceResponse> RemoveWallet(string walletName);
    }
}