namespace Hubtel.UserWallet.Api.ReturnTypes
{
    public class WalletServiceResponse:IWalletServiceResponse
    {
        public bool OperationSuccessful { get; set; }
        public string Message { get; set; }    
    }
}
