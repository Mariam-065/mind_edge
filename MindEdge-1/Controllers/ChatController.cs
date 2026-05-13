using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MindEdge_1.Models;
using MindEdge_1.Services;
using System.Security.Claims;
namespace MindEdge_1.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class ChatController : ControllerBase
    {
        private readonly IChatService _chatService;

        public ChatController(IChatService chatService) => _chatService = chatService;

        private string GetUserId() => User.FindFirst(ClaimTypes.NameIdentifier)?.Value ?? "";

        [HttpPost("send")]
        public async Task<IActionResult> Chat([FromBody] ChatRequestDto request)
        {
            var result = await _chatService.ProcessChatMessageAsync(request.question, request.session_id, request.tts, GetUserId());
            if (result == null) return BadRequest("Session error");

            if (!string.IsNullOrEmpty(result.audio_url))
                result.audio_url = "https://instant-attraction-butler-indicating.trycloudflare.com" + result.audio_url;

            return Ok(result);
        }

        [HttpGet("history/{sessionId}")]
        public async Task<IActionResult> History(string sessionId) => Ok(await _chatService.GetChatHistoryAsync(sessionId, GetUserId()));
    }
}