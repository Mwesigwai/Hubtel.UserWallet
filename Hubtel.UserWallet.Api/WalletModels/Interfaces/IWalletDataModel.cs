namespace Hubtel.UserWallet.Api.WalletModels.Interfaces
{
    public interface IWalletDataModel
    {
        string AccountNumber { get; set; }
        string AccountScheme { get; set; }
        DateTime CreatedAt { get; set; }
        int ID { get; set; }
        string Name { get; set; }
        string Owner { get; set; }
        string Type { get; set; }
    }
}