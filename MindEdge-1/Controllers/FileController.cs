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
                return Unauthorized();

            var result = await _fileService.UploadAsync(model, userId);

            return !string.IsNullOrEmpty(result)
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

        [HttpGet("ListFiles")]
        public async Task<IActionResult> GetFilesAsync()
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier)
                         ?? User.FindFirstValue("sub");

            if (string.IsNullOrEmpty(userId))
                return Unauthorized();

            var files = await _fileService.GetFilesAsync(userId);

            return files != null
                ? Ok(new { files = files })
                : BadRequest();
        }
    }
}