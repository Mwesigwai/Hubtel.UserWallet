namespace Hubtel.UserWallet.Api.ReturnTypes
{
    public interface IServiceResponseFactory<T>where T : class
    {
        T GetResponse(bool operationSucess, string message);
    }
}