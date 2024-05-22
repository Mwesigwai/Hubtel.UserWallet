using Hubtel.UserWallet.Api.WalletModels.WalletEnums;

namespace Hubtel.UserWallet.Api.WalletModels
{
    public interface IWalletModel
    {
        string AccountNumber { get; set; }
        WalletScheme AccountScheme { get; set; }
        DateTime CreatedAt { get; set; }
        int ID { get; set; }
        string Name { get; set; }
        string Owner { get; set; }
        WalletType Type { get; set; }
    }
}