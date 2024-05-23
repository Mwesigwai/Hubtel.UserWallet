namespace Hubtel.UserWallet.Api.ReturnTypes
{
    public class ServiceResponseFactory:IServiceResponseFactory<IWalletServiceResponse>
    {
        public IWalletServiceResponse GetResponse(bool operationSucess, string message)
        {
            return new WalletServiceResponse
            {
                OperationSuccessful = operationSucess,
                Message = message
            };
        }
    }
}
