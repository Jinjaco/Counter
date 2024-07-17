namespace Counter.Authorization.ApiController
{
    public class ApiKeyValidator : IApiKeyValidator
    {
        public bool IsValid(string apiKey)
        {
            return apiKey == "testpasswort";
        }
    }

    public interface IApiKeyValidator
    {
        bool IsValid(string apiKey);
    }
}
