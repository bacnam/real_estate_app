using Microsoft.AspNetCore.Mvc;
using RealEstate.WebService.Services;

namespace RealEstate.WebService.Controllers
{
    [Route("api/addresses")]
    [ApiController]
    public class AddressController : ControllerBase
    {
        IAddressService AddressService;

        public AddressController(IAddressService addressService)
        {
            AddressService = addressService;
        }

        [HttpGet("cities")]
        public async Task<ActionResult> GetCitiesAsync()
        {
            var cities = await AddressService.GetCitiesAsync();
            return new JsonResult(cities);
        }

        [HttpGet("districts/{cityId}")]
        public async Task<ActionResult> GetDistrictsAsync([FromRoute] int cityId)
        {
            var districts = await AddressService.GetDistrictsAsync(cityId);
            return new JsonResult(districts);
        }

        [HttpGet("directions")]
        public async Task<ActionResult> GetDirectionsAsync()
        {
            var huong = await AddressService.GetDirectionsAsync();
            return new JsonResult(huong);
        }
    }
}
