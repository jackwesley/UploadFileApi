using Microsoft.AspNetCore.Mvc;

namespace UploadFileApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class UploadFileController : Controller
    {
        private readonly IWebHostEnvironment _webHostEnvironment;
        public UploadFileController(IWebHostEnvironment webHostEnvironment)
        {
            _webHostEnvironment = webHostEnvironment;
        }

        [HttpPost("upload")]
        public async Task<IActionResult> UploadFile(IFormFile file)
        {
            if (file == null)
                return BadRequest();

            string directoryPath = Path.Combine(_webHostEnvironment.ContentRootPath, "UploadedFiles");
            string filePath = Path.Combine(directoryPath, file.FileName);
            using (var stream = new FileStream(filePath, FileMode.Create))
            {
                await file.CopyToAsync(stream);
            }

            return Ok($"File Uploaded to {directoryPath}{filePath}");
        }
    }
}
