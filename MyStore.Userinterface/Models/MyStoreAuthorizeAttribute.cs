using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc;
using MyStore.Service;
using MyStore.Database.Models.Authentication;

namespace MyStore.Userinterface.Models
{
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method, AllowMultiple = true, Inherited = true)]
    public class MyStoreAuthorizeAttribute : Attribute, IAuthorizationFilter
    {
        private readonly string[]? _roles;
        public MyStoreAuthorizeAttribute(params string[]? Roles)
        {
            _roles = Roles;
        }

        public void OnAuthorization(AuthorizationFilterContext context)
        {
            try
            {
                var httpClientFactory = context.HttpContext.RequestServices.GetService(typeof(IHttpClientFactory)) as IHttpClientFactory;
                var _authenticateRestClient = new AuthenticateRestClient<AuthenticationResponse>(httpClientFactory.CreateClient("AuthenticationAPI"));

                var tokenCookie = context.HttpContext.Request.Cookies["token"];
                if (!string.IsNullOrEmpty(tokenCookie))
                {
                    var result = _authenticateRestClient.Post("validate-token", tokenCookie, tokenCookie).GetAwaiter().GetResult();
                    if (result.IsSuccess && result.Result.IsAuthenticated)
                    {
                        if (_roles != null && _roles.Any())
                        {
                            var userRole = _authenticateRestClient.Get($"user-roles", tokenCookie).GetAwaiter().GetResult();
                            if (userRole.IsSuccess && userRole.Result.Roles.Any(x => _roles.Contains(x)))
                                return;
                        }
                        return;
                    }
                }
                context.Result = new RedirectToRouteResult(new
             RouteValueDictionary(new { controller = "Home", action = "Login" }));
                return;
            }
            catch (Exception)
            {
                context.Result = new RedirectToRouteResult(new
            RouteValueDictionary(new { controller = "Home", action = "Login" }));
                return;
            }
        }
    }
}
