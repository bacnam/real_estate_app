using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using RealEstate.WebService.Extensions;
using RealEstate.WebService.Models;
using RealEstate.WebService.Services;

namespace RealEstate.WebService.Attributes
{
    public class AccountAuthorizeFilter : IAuthorizationFilter
    {
        protected IUserService UserService;
        protected UserModel User;

        public AccountAuthorizeFilter(IUserService userService)
        {
            UserService = userService;
        }

        public virtual void OnAuthorization(AuthorizationFilterContext context)
        {
            string token = context.HttpContext.GetToken();
            if (!string.IsNullOrEmpty(token))
            {
                token = token.TrimStart();
                User = UserService.GetAccountFromToken(token);
                if (User == null)
                {
                    context.Result = new UnauthorizedResult();
                }
            }
            else
            {
                context.Result = new UnauthorizedResult();
            }
        }
    }
}
