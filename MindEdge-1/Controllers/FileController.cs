using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using MindEdge_1.Models;
using MindEdge_1.Services;
using System.IO;
using System.Threading.Tasks;



namespace MindEdge_1.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FileController: ControllerBase
    {
        private readonly IFileService _fileService;

        public FileController(IFileService fileService)
        {
            _fileService = fileService;
        }

        [HttpPost("Upload")]
        public async Task<IActionResult> UploadAsync(FileUploadDto model)
        {
            var result = await _fileService.UploadAsync(model);
            return !string.IsNullOrEmpty(result) ? Ok(new { message = true }) : BadRequest(new { message = false });

        }
        [HttpPost("Download")]
        public async Task<IActionResult> DownloadAsync(string fileName)
        {
            var result = await _fileService.DownloadAsync(fileName);
            return result != null ? Ok(new {file = result }) : BadRequest();
        }
        [HttpGet("ListFiles")]
        public async Task<IActionResult> GetFilesAsync()
        {
            var _files = await _fileService.GetFilesAsync();
            return _files != null? Ok(new { files = _files }  ) : BadRequest();
        }
    }
}
