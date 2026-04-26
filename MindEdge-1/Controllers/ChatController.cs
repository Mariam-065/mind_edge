    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;
    using MindEdge_1.Data;
    using MindEdge_1.Models;
    using System.Security.Claims;

    [Authorize] // عشان نضمن إن الطالب عامل Login
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
            // 1. نجيب الـ UserId من الـ Token اللي إنتي برمجتيه
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            // 2. نكلم سيرفر الـ AI ونبعت الـ question والـ session_id (اللي هو الـ Doc Name أو ID)
            // ملاحظة: بايثون مستني session_id، إحنا هنبعتله اسم الملف أو الـ Id بتاعه
            var aiResponseData = await _apiClient.AskQuestionAsync(request.Question, request.SessionId);

            // 3. نسجل الرد في الـ Model بتاعك (AIResponse)
            var aiEntry = new AIResponse
            {
                ResponseText = aiResponseData.answer,
                CreatedAt = DateTime.Now,
                // هنا بنربطها بالـ Session (الغرفة) لو حابة، أو نسيبها كدة للتجربة
                DetectedIntent = "Study Query"
            };

            _context.AIResponses.Add(aiEntry);
            await _context.SaveChangesAsync();

            // 4. نرجع الرد للفلاتر عشان يظهر للطالب
            return Ok(new { answer = aiEntry.ResponseText, sessionId = aiResponseData.session_id });
        }
    }

