using Microsoft.AspNetCore.Mvc;
using MindEdge_1.Models;

namespace MindEdge_1.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ChatController : ControllerBase
    {
        private readonly ExternalApiClient _externalApiClient;

        public ChatController(ExternalApiClient externalApiClient)
        {
            _externalApiClient = externalApiClient;
        }

        [HttpPost("send")]
        public async Task<IActionResult> Chat([FromBody] ChatRequestDto request)
        {
            try
            {
                var result = await _externalApiClient.SendChatMessageAsync(request);

                // لو فيه لينك صوت، بنحوله للينك كامل عشان الفلاتر تعرف تشغله
                if (!string.IsNullOrEmpty(result.audio_url))
                {
                    // بنجيب الـ BaseAddress بتاع الـ AI سيرفر
                    string aiBaseUrl = "https://instant-attraction-butler-indicating.trycloudflare.com";
                    result.audio_url = aiBaseUrl + result.audio_url;
                }

                return Ok(result);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = ex.Message });
            }
        }
    }
}