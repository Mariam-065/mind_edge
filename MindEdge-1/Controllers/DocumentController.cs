using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
<<<<<<< HEAD
using Microsoft.EntityFrameworkCore;
=======
>>>>>>> 1fa47f3bc21612e4eb00a3627a06e188035cf1ee
using MindEdge_1.Data;
using MindEdge_1.Models;
using MindEdge_1.Services;
using QuestPDF.Fluent;
using QuestPDF.Helpers;
using QuestPDF.Infrastructure;
using System.Security.Claims;
using System.Text.Json;
<<<<<<< HEAD
using System.Security.Claims;
=======
using System.Text.Json.Nodes;
namespace MindEdge_1.Controllers;
>>>>>>> 1fa47f3bc21612e4eb00a3627a06e188035cf1ee


[Authorize]
[ApiController]
[Route("api/[controller]")]
public class DocumentController : ControllerBase
{
    private readonly IFileService _fileService;
    private readonly ExternalApiClient _apiClient;
    private readonly ApplicationDbContext _context;
    private readonly IConfiguration _configuration;
    public DocumentController(IFileService fileService, ExternalApiClient apiClient, ApplicationDbContext context,
        IStudyPlanService studyPlanService, IConfiguration configuration)
    {
<<<<<<< HEAD
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
=======
        _fileService = fileService;
        _apiClient = apiClient;
        _context = context;
        _configuration = configuration;
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

        var jsonDoc = JsonNode.Parse(analysisResult);
        
        var graphs = jsonDoc["graphs"]?.AsArray();

        foreach(var graph in graphs)
            graph["image"]= $"{_configuration["AIUrl"] ?? ""}/{filename}";
        

        jsonDoc["session_id"] = sessionId;

        return Ok(jsonDoc);
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
        return Ok(  rules );
    }

    [HttpGet("get-definitions")]
    public async Task<IActionResult> GetDefinitions(string filename)
    {
        var definitions = await _apiClient.GetDocumentDataAsync("definitions", filename);
        return Ok( definitions );
    }

    [HttpPost("DownloadSummaryPdf")]
    public IActionResult DownloadSummaryPdf([FromBody] SummaryRequestDto model)
    {
        if (string.IsNullOrEmpty(model.Content))
            return BadRequest("Content is empty");

        var pdfBytes = QuestPDF.Fluent.Document.Create(container =>
        {
            container.Page(page =>
            {
                page.Size(PageSizes.A4);
                page.Margin(1, Unit.Centimetre);
                page.PageColor(Colors.White);
                page.DefaultTextStyle(x => x.FontSize(12));

                page.Header().Text(model.Title)
                    .SemiBold().FontSize(20).FontColor(Colors.Blue.Medium);

                page.Content().PaddingVertical(10).Column(column =>
                {
                    column.Spacing(5);
                    column.Item().Text(model.Content);
                });

                page.Footer().AlignCenter().Text(x =>
                {
                    x.Span("Page ");
                    x.CurrentPageNumber();
                });
            });
        }).GeneratePdf();
        return File(pdfBytes, "application/pdf", $"{model.Title}_Summary.pdf");
    }

  



}
>>>>>>> 1fa47f3bc21612e4eb00a3627a06e188035cf1ee
