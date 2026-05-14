using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MindEdge_1.Models;
using MindEdge_1.Services;
<<<<<<< HEAD
using System.Security.Claims;
=======
using System.Security.Claims; 
>>>>>>> 1fa47f3bc21612e4eb00a3627a06e188035cf1ee

namespace MindEdge_1.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class FileController : ControllerBase
    {
        private readonly IFileService _fileService;

        public FileController(IFileService fileService)
        {
            _fileService = fileService;
        }

        [HttpPost("Upload")]
        public async Task<IActionResult> UploadAsync([FromForm] FileUploadDto model)
        {
<<<<<<< HEAD
=======
            // سحب الـ ID من التوكن
>>>>>>> 1fa47f3bc21612e4eb00a3627a06e188035cf1ee
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier)
                         ?? User.FindFirstValue("sub");

            if (string.IsNullOrEmpty(userId))
<<<<<<< HEAD
                return Unauthorized();
=======
                return Unauthorized(new { message = "User ID not found in token" });
>>>>>>> 1fa47f3bc21612e4eb00a3627a06e188035cf1ee

            var result = await _fileService.UploadAsync(model, userId);

            return !string.IsNullOrEmpty(result)
<<<<<<< HEAD
                ? Ok(new { message = true })
                : BadRequest(new { message = false });
        }

        [HttpPost("Download")]
        public async Task<IActionResult> DownloadAsync(string fileName)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier)
                         ?? User.FindFirstValue("sub");

            if (string.IsNullOrEmpty(userId))
                return Unauthorized();

            var result = await _fileService.DownloadAsync(fileName, userId);

            return result != null
                ? Ok(new { file = result })
                : BadRequest();
        }

=======
                ? Ok(new { message = true, fileName = result })
                : BadRequest(new { message = false, error = "Upload failed" });
        }

>>>>>>> 1fa47f3bc21612e4eb00a3627a06e188035cf1ee
        [HttpGet("ListFiles")]
        public async Task<IActionResult> GetFilesAsync()
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier)
                         ?? User.FindFirstValue("sub");

            if (string.IsNullOrEmpty(userId))
<<<<<<< HEAD
                return Unauthorized();

            var files = await _fileService.GetFilesAsync(userId);

            return files != null
                ? Ok(new { files = files })
                : BadRequest();
=======
                return Unauthorized(new { message = "User ID not found in token" });

            var filesList = await _fileService.GetFilesAsync(userId);

            return Ok(new { files = filesList });
        }

        [HttpPost("Download")]
        public async Task<IActionResult> DownloadAsync([FromQuery] string fileName)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier)
                         ?? User.FindFirstValue("sub");

            if (string.IsNullOrEmpty(userId))
                return Unauthorized();

            var result = await _fileService.DownloadAsync(fileName, userId);

            return result != null
                ? File(result, "application/octet-stream", fileName)
                : BadRequest(new { error = "File not found" });
>>>>>>> 1fa47f3bc21612e4eb00a3627a06e188035cf1ee
        }
    }
}