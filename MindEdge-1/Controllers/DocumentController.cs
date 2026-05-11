using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MindEdge_1.Data;
using MindEdge_1.Models;
using MindEdge_1.Services;
using System.Text.Json;
using System.Security.Claims;

namespace MindEdge_1.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/[controller]")]
    public class DocumentController : ControllerBase
    {
        private readonly IFileService _fileService;
        private readonly ExternalApiClient _apiClient;
        private readonly ApplicationDbContext _context;

        public DocumentController(IFileService fileService, ExternalApiClient apiClient, ApplicationDbContext context)
        {
            _fileService = fileService;
            _apiClient = apiClient;
            _context = context;
        }

        [HttpPost("analyze-visuals")]
        public async Task<IActionResult> AnalyzeVisuals(IFormFile file)
        {
            if (file == null || file.Length == 0) return BadRequest("No file uploaded.");

            var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value ?? "";
            var fileDto = new FileUploadDto { File = file };
            var filename = await _fileService.UploadAsync(fileDto, userId);

            string sessionId = Guid.NewGuid().ToString();

            var session = new ChatbotRoom { SessionId = sessionId, FileName = filename };
            _context.ChatbotRooms.Add(session);
            await _context.SaveChangesAsync();

            using var stream = file.OpenReadStream();
            var analysisResult = await _apiClient.AnalyzeDocumentAsync(stream, filename);
            
            var jsonDoc = JsonDocument.Parse(analysisResult);
            var dict = new Dictionary<string, object>();
            
            foreach (var property in jsonDoc.RootElement.EnumerateObject())
                dict[property.Name] = property.Value;
            
            dict["session_id"] = sessionId;
            
            return Ok(JsonSerializer.Serialize(dict));
        }

        [HttpPost("summary")]
        public async Task<IActionResult> Summary([FromQuery] string fileName, [FromQuery] bool tts = false)
        {
            var summary = await _apiClient.GetSummaryAsync(fileName, tts);
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