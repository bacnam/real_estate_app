using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RealEstate.Core.Requests;
using RealEstate.WebService.Attributes;
using RealEstate.WebService.Extensions;
using RealEstate.WebService.Services;

namespace RealEstate.WebService.Controllers
{
    [Route("api/users")]
    [ApiController]
    public class UserController : ControllerBase
    {
        IUserService Service;
        public UserController(IUserService userService)
        {
            this.Service = userService;
        }


        [HttpPost("login")]
        public async Task<ActionResult> LoginAsync([FromBody] LoginRequest request)
        {
            var response = await Service.LoginAsync(request);
            return new JsonResult(response);
        }

        [HttpPost("facebook/login")]
        public async Task<ActionResult> LoginFacebookAsync([FromBody] FacebookLoginRequest request)
        {
            var response = await Service.DoLoginFacebookAsync(request.Token);
            return new JsonResult(response);
        }

        [HttpPost("google/login")]
        public async Task<ActionResult> LoginGoogleAsync([FromBody] GoogleLoginRequest request)
        {
            var response = await Service.DoLoginGoogleAsync(request.Token);
            return new JsonResult(response);
        }

        [HttpPost("register")]
        public async Task<ActionResult> RegisterAsync([FromBody] RegisterRequest request)
        {
            var response = await Service.RegisterAsync(request);
            return new JsonResult(response);
        }

        [HttpPost, AccountAuthorize]
        public async Task<ActionResult> GetAccountAsync()
        {
            var token = this.HttpContext.GetToken();
            var response = await Service.GetAccountAsync(token);
            return new JsonResult(response);
        }

        [HttpPost("logout"), AccountAuthorize]
        public ActionResult LogoutAsync()
        {
            var token = this.HttpContext.GetToken();
            Service.LogoutAsync(token);
            return new OkResult();
        }

        [HttpGet("notifications"), AccountAuthorize]
        public async Task<ActionResult> GetNotificationAsync()
        {
            var account = Service.GetAccountFromToken(HttpContext.GetToken());
            if (account != null)
            {
                var response = await Service.GetNotificationAsync(account.Id);
                return new JsonResult(response);
            }
            return new UnauthorizedResult();
        }

        [HttpPut("notifications"), AccountAuthorize]
        public async Task<ActionResult> ReadNotificationAsync([FromBody] ReadNotificationRequest request)
        {
            var account = Service.GetAccountFromToken(HttpContext.GetToken());
            if (account != null)
            {
                var response = await Service.ReadNotificationAsync(request.Id);
                return new JsonResult(response);
            }
            return new UnauthorizedResult();
        }
    }
}
