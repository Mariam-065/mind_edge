using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MindEdge_1.Models;

namespace MindEdge_1.Controllers
{
    [Authorize]
    [ApiController]
    [Route("[controller]")]
    public class DocumentController : ControllerBase
    {
        private readonly ExternalApiClient _apiClient;

        public DocumentController(ExternalApiClient apiClient)
        {
            _apiClient = apiClient;
        }

        [HttpPost("analyze")]
        public async Task<IActionResult> Analyze(IFormFile file)
        {
            if (file == null || file.Length == 0)
                return BadRequest("No file uploaded.");

            using var stream = file.OpenReadStream();
            var result = await _apiClient.AnalyzeDocumentAsync(stream, file.FileName);
            return Ok(result);
        }

        [HttpGet("summary")]
        public async Task<IActionResult> Summary([FromQuery] string filename)
        {
            var result = await _apiClient.GetSummaryAsync(filename);
            return Ok(result);
        }

        [HttpGet("graphs")]
        public async Task<IActionResult> Graphs()
        {
            var result = await _apiClient.GetGraphsAsync();
            return Ok(result);
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
