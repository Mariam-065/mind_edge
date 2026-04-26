    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;
    using MindEdge_1.Data;
    using MindEdge_1.Models;
    using System.Security.Claims;

    [Authorize] 
    [ApiController]
    [Route("api/[controller]")]
    public class ChatController : ControllerBase
    {
        private readonly ExternalApiClient _apiClient;
        private readonly ApplicationDbContext _context;

        public ChatController(ExternalApiClient apiClient, ApplicationDbContext context)
        {
            _apiClient = apiClient;
            _context = context;
        }

        [HttpPost("send-message")]
        public async Task<IActionResult> SendMessage([FromBody] ChatRequestDto request)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var aiResponseData = await _apiClient.AskQuestionAsync(request.Question, request.SessionId);
            var aiEntry = new AIResponse
            {
                ResponseText = aiResponseData.answer,
                CreatedAt = DateTime.Now,
                DetectedIntent = "Study Query"
            };

            _context.AIResponses.Add(aiEntry);
            await _context.SaveChangesAsync();

            return Ok(new { answer = aiEntry.ResponseText, sessionId = aiResponseData.session_id });
        }
    }

