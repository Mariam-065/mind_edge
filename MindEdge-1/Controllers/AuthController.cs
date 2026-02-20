using Microsoft.AspNetCore.Mvc;
using MindEdge_1.Models;
using MindEdge_1.Services;

namespace MindEdge_1.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService _authService;

        // هنا بنعمل حقن للخدمة عشان الكنترولر يقدر يستخدمها
        public AuthController(IAuthService authService)
        {
            _authService = authService;
        }

        // رابط التسجيل: api/Auth/register
        [HttpPost("register")]
        public async Task<IActionResult> Register(User user)
        {
            var result = await _authService.Register(user);
            if (result) return Ok(new { message = "تم إنشاء الحساب بنجاح!" });
            return BadRequest("فشل في إنشاء الحساب.");
        }

        // رابط الدخول: api/Auth/login
        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginRequest request)
        {
            var user = await _authService.Login(request.Email, request.Password);
            if (user == null) return Unauthorized("الإيميل أو كلمة السر خطأ.");

            return Ok(new { message = "تم تسجيل الدخول بنجاح!", userName = user.Name });
        }
    }

    // كلاس بسيط لاستلام بيانات اللوجن فقط
    public class LoginRequest
    {
        public string Email { get; set; }
        public string Password { get; set; }
    }
}