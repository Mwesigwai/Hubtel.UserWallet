using Hubtel.UserWallet.Api.WalletModels.WalletEnums;

namespace Hubtel.UserWallet.Api.WalletModels
{
    public interface IWalletPostModel
    {
        string AccountNumber { get; set; }
        WalletScheme AccountScheme { get; set; }
        string Name { get; set; }
        string Owner { get; set; }
        WalletType WalletType { get; set; }
    }
}