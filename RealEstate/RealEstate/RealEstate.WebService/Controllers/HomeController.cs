using Microsoft.AspNetCore.Mvc;

namespace RealEstate.WebService.Controllers
{
    [Route("api/addresses")]
    [ApiController]
    public class HomeController : ControllerBase
    {
        public ActionResult Index()
        {
            //return new EmptyResult();
            return new OkObjectResult("ok");
        }
    }
}
