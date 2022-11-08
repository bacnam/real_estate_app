using Microsoft.AspNetCore.Mvc;
using RealEstate.Core.Requests;
using RealEstate.WebService.Attributes;
using RealEstate.WebService.Services;

namespace RealEstate.WebService.Controllers
{
    [Route("api/news")]
    [ApiController]
    public class NewsController : ControllerBase
    {
        INewsService NewsService;
        IUserService AccountService;

        public NewsController(INewsService newsService, IUserService accountService)
        {
            AccountService = accountService;
            NewsService = newsService;
        }

        [HttpPost(), AccountAuthorize]
        public async Task<ActionResult> GetNewsAsync([FromBody] GetNewsRequest request)
        {
            var response = await NewsService.GetNewsAsync(request.Start, request.IsMarket);
            return new JsonResult(response);
        }

        [HttpGet("{newsId}"), AccountAuthorize]
        public async Task<ActionResult> GetNewsDetailsAsync([FromRoute] long newsId)
        {
            var response = await NewsService.GetNewsDetailsAsync(newsId);
            return new JsonResult(response);
        }

        [HttpPost("register"), AccountAuthorize]
        public async Task<ActionResult> RegisterNewsAsync([FromBody] RegisterNewsRequest request)
        {
            var response = await NewsService.RegisterNewsAsync(request);
            return new JsonResult(response);
        }
    }
}
