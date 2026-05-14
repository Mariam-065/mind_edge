<<<<<<< HEAD
﻿using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MindEdge_1.Data;
=======
﻿using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
>>>>>>> 1fa47f3bc21612e4eb00a3627a06e188035cf1ee
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
<<<<<<< HEAD
        private readonly ExternalApiClient _externalApiClient;
        private readonly ApplicationDbContext _context;

        public ChatController(ExternalApiClient externalApiClient, ApplicationDbContext context)
        {
            _externalApiClient = externalApiClient;
            _context = context;
        }
=======
        private readonly IChatService _chatService;

        public ChatController(IChatService chatService) => _chatService = chatService;

        private string GetUserId() => User.FindFirst(ClaimTypes.NameIdentifier)?.Value ?? "";
>>>>>>> 1fa47f3bc21612e4eb00a3627a06e188035cf1ee

        [HttpPost("chat")]
        public async Task<IActionResult> Chat(string question, string sessionId, bool tts = false)
        {
<<<<<<< HEAD
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
=======
            var result = await _chatService.ProcessChatMessageAsync(request.question, request.session_id, request.tts, GetUserId());
            if (result == null) return BadRequest("Session error");

            if (!string.IsNullOrEmpty(result.audio_url))
                result.audio_url = "https://instant-attraction-butler-indicating.trycloudflare.com" + result.audio_url;

            return Ok(result);
>>>>>>> 1fa47f3bc21612e4eb00a3627a06e188035cf1ee
        }

        [HttpGet("history/{sessionId}")]
        public async Task<IActionResult> History(string sessionId) => Ok(await _chatService.GetChatHistoryAsync(sessionId, GetUserId()));
    }
}