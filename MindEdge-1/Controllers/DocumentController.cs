using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MindEdge_1.Models;
using MindEdge_1.Services;
using System.Text.Json;
using System.Linq;

namespace MindEdge_1.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/[controller]")]
    public class DocumentController : ControllerBase
    {
        private readonly IFileService _fileService;
        private readonly ExternalApiClient _apiClient;

        public DocumentController(IFileService fileService, ExternalApiClient apiClient)
        {
            _fileService = fileService;
            _apiClient = apiClient;
        }

       [HttpPost("analyze-visuals")]
        public async Task<IActionResult> AnalyzeVisuals(IFormFile file)
        {
            if (file == null || file.Length == 0) return BadRequest("No file uploaded.");
            
            var fileDto = new FileUploadDto { File = file };
            var filename = await _fileService.UploadAsync(fileDto); 
            
            using var stream = file.OpenReadStream();
            
            var analysisResult = await _apiClient.AnalyzeDocumentAsync(stream, filename);
            return Ok(analysisResult);
        }
        [HttpPost("summary")]
        public async Task<IActionResult> Summary([FromQuery] string fileName, [FromQuery] bool tts = false)
        {

            var summary = await _apiClient.GetSummaryAsync(fileName,tts);
            return Ok(summary);
        }

        [HttpGet("get-rules")]
        public async Task<IActionResult> GetRules(string filename)
        { 
            var rules = await _apiClient.GetDocumentDataAsync("rules", filename);
            return Ok(new { rules });
        }

        [HttpGet("get-definitions")]
        public async Task<IActionResult> GetDefinitions(string filename)
        {
            var definitions = await _apiClient.GetDocumentDataAsync("definitions", filename);
            return Ok(new { definitions });
        }
    }
}
