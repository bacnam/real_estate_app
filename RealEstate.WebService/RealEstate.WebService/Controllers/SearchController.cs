using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using RealEstate.Core.Requests;
using RealEstate.WebService.Attributes;
using RealEstate.WebService.Extensions;
using RealEstate.WebService.Services;

namespace RealEstate.WebService.Controllers
{
    [Route("api/search")]
    [ApiController]
    public class SearchController : ControllerBase
    {
        ISearchService SearchService;
        IAddressService AddressService;
        IUserService AccountService;
        IProjectService ProjectService;

        public SearchController(ISearchService searchService,
            IAddressService addressService,
            IProjectService projectService,
            IUserService accountService)
        {
            SearchService = searchService;
            AddressService = addressService;
            AccountService = accountService;
            ProjectService = projectService;
        }

        [HttpGet("cities")]
        public async Task<ActionResult> GetCitiesAsync()
        {
            var cities = await AddressService.GetCitiesAsync();
            return new JsonResult(cities);
        }

        [HttpGet("districts/{city}")]
        public async Task<ActionResult> GetDistrictsAsync([FromRoute] string city)
        {
            var cities = city.Split(",").Select(s => int.Parse(s)).ToArray();
            var districts = await AddressService.GetDistrictsAsync(cities);
            return new JsonResult(districts);
        }

        [HttpGet("wards/{district}")]
        public async Task<ActionResult> GetWardsAsync([FromRoute] string district)
        {
            var districts = district.Split(",").Select(s => int.Parse(s)).ToArray();
            var wards = await AddressService.GetWardsAsync(districts);
            return new JsonResult(wards);
        }

        [HttpGet("projects")]
        public async Task<ActionResult> GetProjectsAsync()
        {
            var projects = await ProjectService.GetProjectsAsync();
            Console.WriteLine(JsonConvert.SerializeObject(projects));
            return new JsonResult(projects);
        }

        [HttpGet("streets/{wardId}")]
        public async Task<ActionResult> GetStreetsAsync([FromRoute] int wardId)
        {
            var districts = await AddressService.GetStreetsAsync(wardId);
            return new JsonResult(districts);
        }

        [HttpGet("directions")]
        public async Task<ActionResult> GetDirectionsAsync()
        {
            var directions = await AddressService.GetDirectionsAsync();
            return new JsonResult(directions);
        }

        [HttpGet("rooms")]
        public async Task<ActionResult> GetRoomsAsync()
        {
            var rooms = await AddressService.GetRoomsAsync();
            return new JsonResult(rooms);
        }

        [HttpPost, AccountAuthorize]
        public async Task<ActionResult> SearchAsync([FromBody] SearchRequest request)
        {
            Console.WriteLine(JsonConvert.SerializeObject(request));
            var account = AccountService.GetAccountFromToken(HttpContext.GetToken());
            if (account != null)
            {
                var data = await SearchService.SearchAsync(account.Id,
                    request.City,
                    request.District,
                    request.Ward,
                    request.RealTypes,
                    request.Room,
                    request.FromPrice,
                    request.ToPrice,
                    request.FromAcreage,
                    request.ToAcreage,
                    request.IsSale,
                    request.Start);
                return new JsonResult(data);
            }
            return new UnauthorizedResult();
        }

        [HttpPost("project"), AccountAuthorize]
        public async Task<ActionResult> SearchByProjectAsync([FromBody] SearchRequest request)
        {
            var account = AccountService.GetAccountFromToken(HttpContext.GetToken());
            if (account != null)
            {
                var data = await SearchService.SearchByProjectAsync(account.Id, request.Project, request.Start);
                return new JsonResult(data);
            }
            return new UnauthorizedResult();
        }

        [HttpPost("address"), AccountAuthorize]
        public async Task<ActionResult> SearchAddressAsync(SearchAddressRequest request)
        {
            var account = AccountService.GetAccountFromToken(HttpContext.GetToken());
            if (account != null)
            {
                var data = await SearchService.SearchAddressAsync(request.Address);
                return new JsonResult(data);
            }
            return new UnauthorizedResult();
        }
    }
}