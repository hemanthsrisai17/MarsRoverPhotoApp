using Microsoft.AspNetCore.Mvc;

namespace MarsRoverPhotoApp.Gallery.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ImagesController : ControllerBase
    {
        private readonly string _imageDirectory;

        public ImagesController(IConfiguration configuration)
        {
            _imageDirectory = configuration["NASA:DestinationFolder"];
        }

        [HttpGet]
        public ActionResult<IEnumerable<string>> Get()
        {
            if (!Directory.Exists(_imageDirectory))
            {
                return NotFound("Image directory not found.");
            }

            var imageUrls = Directory.GetFiles(_imageDirectory)
                                    .Select(file => Path.GetFileName(file))
                                    .Select(fileName => $"/api/images/{fileName}") // Adjust the base URL as needed
                                    .ToList();

            return Ok(imageUrls);
        }
    }

}