namespace Hubtel.UserWallet.Api.ReturnTypes
{
    public interface IWalletServiceResponse
    {
        bool OperationSuccessful { get; set; }
        string Message { get; set; }
    }
}