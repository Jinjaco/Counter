using Microsoft.AspNetCore.Mvc;

namespace Counter.Authorization.ApiController
{
    public class ApiKeyAttribute : ServiceFilterAttribute
    {
        public ApiKeyAttribute()
            : base(typeof(ApiKeyAuthorizationFilter))
        {
        }
    }
}
