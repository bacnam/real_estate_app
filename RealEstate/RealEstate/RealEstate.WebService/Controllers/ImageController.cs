using Microsoft.AspNetCore.Mvc;
using RealEstate.WebService.Services;

namespace RealEstate.WebService.Controllers
{
    [Route("api/images")]
    [ApiController]
    public class ImageController : ControllerBase
    {
        public ImageController()
        {
        }

        [HttpGet("{filename}")]
        public async Task<ActionResult> GetImageAsync([FromRoute] string filename, [FromServices] IImageService imageService)
        {
            var stream = await imageService.GetImage(filename);
            if (stream != null)
            {
                return new FileStreamResult(stream, "image/jpeg");
            }
            return new NotFoundResult();
        }
    }
}
