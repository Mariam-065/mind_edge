using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MindEdge_1.Data;
using MindEdge_1.Models;

namespace MindEdge_1.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ChatController : ControllerBase
    {
        private readonly ExternalApiClient _externalApiClient;
        private readonly ApplicationDbContext _context;

        public ChatController(ExternalApiClient externalApiClient, ApplicationDbContext context)
        {
            _externalApiClient = externalApiClient;
            _context = context;
        }

        [HttpPost("chat")]
        public async Task<IActionResult> Chat(string question, string sessionId, bool tts = false)
        {
            var session = await _context.ChatbotRooms.FirstOrDefaultAsync(s => s.SessionId == sessionId);

            if (session == null) return BadRequest("No file has been uploaded.!");

            var chatRequest = new ChatRequestDto
            {
                question = question,
                filename = session.FileName, 
                session_id = sessionId,
                tts = tts ,
                tts_source = "response"
            };

            var aiResponse = await _externalApiClient.SendChatMessageAsync(chatRequest);
            if (aiResponse != null && !string.IsNullOrEmpty(aiResponse.audio_url))
            {
                string aiBaseUrl = "https://instant-attraction-butler-indicating.trycloudflare.com";

                aiResponse.audio_url = aiBaseUrl + aiResponse.audio_url;
            }
            return Ok(aiResponse);
        }
    }
}