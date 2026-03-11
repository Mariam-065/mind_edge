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
        private readonly IFileService _fileServaice;

        public FileController(IFileService fileServaice)
        {
            _fileServaice = fileServaice;
        }

        [HttpPost("Upload")]
        public async Task<IActionResult> UploadAsync(FileUploadDto model)
        {
            var result = await _fileServaice.UploadAsync(model);
            return result == true ? Ok(new { message = result }) : BadRequest(new { message = result });

        }
        [HttpPost("Download")]
        public async Task<IActionResult> DownloadAsync(string fileName)
        {
            var result = await _fileServaice.DownloadAsync(fileName);
            return result != null ? Ok(new {file = result }) : BadRequest();
        }
        [HttpPost("ListFiles")]
        public async Task<IActionResult> GetFilesAsync()
        {
            var _files = await _fileServaice.GetFilesAsync();
            return _files != null? Ok(new { files = _files }  ) : BadRequest();
        }



    }
}
