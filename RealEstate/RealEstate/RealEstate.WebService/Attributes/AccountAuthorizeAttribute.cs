using Microsoft.AspNetCore.Mvc;

namespace RealEstate.WebService.Attributes
{
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method)]
    public class AccountAuthorizeAttribute : TypeFilterAttribute
    {
        public AccountAuthorizeAttribute()
            : base(typeof(AccountAuthorizeFilter))
        {
            Arguments = new object[]
            {
            };
        }
    }
}
