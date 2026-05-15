using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MindEdge_1.Models;
using MindEdge_1.Services;
using System.Security.Claims; 

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
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier)
                         ?? User.FindFirstValue("sub");

            if (string.IsNullOrEmpty(userId))
                return Unauthorized(new { message = "User ID not found in token" });

            var result = await _fileService.UploadAsync(model, userId);

            return !string.IsNullOrEmpty(result)
                ? Ok(new { message = true, fileName = result })
                : BadRequest(new { message = false, error = "Upload failed" });
        }

        [HttpGet("ListFiles")]
        public async Task<IActionResult> GetFilesAsync()
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier)
                         ?? User.FindFirstValue("sub");

            if (string.IsNullOrEmpty(userId))
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
        }
    }
}