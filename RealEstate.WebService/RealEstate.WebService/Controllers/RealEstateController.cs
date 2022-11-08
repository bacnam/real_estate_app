using Microsoft.AspNetCore.Mvc;
using RealEstate.Core.Requests;
using RealEstate.Core.Responses;
using RealEstate.WebService.Attributes;
using RealEstate.WebService.Extensions;
using RealEstate.WebService.Services;

namespace RealEstate.WebService.Controllers
{
    [Route("api/realestates")]
    [ApiController]
    public class RealEstateController : ControllerBase
    {
        IRealEstateService RealEstateService;
        IUserService AccountService;

        public RealEstateController(IRealEstateService realEstateService, IUserService accountService)
        {
            RealEstateService = realEstateService;
            AccountService = accountService;
        }

        [HttpPut, AccountAuthorize]
        public async Task<ActionResult> ChangeRealEstate([FromBody] ChangeRealEstateRequest request)
        {
            var account = AccountService.GetAccountFromToken(HttpContext.GetToken());
            var data = await RealEstateService.ChangeRealEstatesSave(account.Id, request.RealEstateId);
            ChangeRealEstateResponse response = new ChangeRealEstateResponse();
            if (data)
            {
                response.Success = true;
            }
            return new JsonResult(response);
        }

        [HttpGet("saved"), AccountAuthorize]
        public async Task<ActionResult> GetSavedRealEstates([FromQuery] int start = 0)
        {
            var account = AccountService.GetAccountFromToken(HttpContext.GetToken());
            var response = await RealEstateService.GetSavedRealEstates(account.Id, start);
            return new JsonResult(response);
        }

        [HttpGet("{realEstateId}"), AccountAuthorize]
        public async Task<ActionResult> GetRealEstate([FromRoute] long realEstateId)
        {
            var account = AccountService.GetAccountFromToken(HttpContext.GetToken());
            var response = await RealEstateService.GetRealEstate(account.Id, realEstateId);
            return new JsonResult(response);
        }

        [HttpPost, AccountAuthorize]
        public async Task<ActionResult> CreateRealEstate([FromBody] CreateEstateRequest request)
        {
            var account = AccountService.GetAccountFromToken(HttpContext.GetToken());
            var response = await RealEstateService.CreateRealEstate(account.Id, request);
            return new JsonResult(response);
        }

        [HttpGet, AccountAuthorize]
        public async Task<ActionResult> GetMyRealEstatesAsync([FromQuery] int sale)
        {
            var account = AccountService.GetAccountFromToken(HttpContext.GetToken());
            var response = await RealEstateService.GetMyRealEstatesAsync(account.Id, sale == 1);
            return new JsonResult(response);
        }
    }
}
