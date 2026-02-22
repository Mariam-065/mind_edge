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

        public AuthController(IAuthService authService)
        {
            _authService = authService;
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register(User user)
        {
            var result = await _authService.Register(user);
            if (result) return Ok(new { message = "تم إنشاء الحساب بنجاح!" });
            return BadRequest("فشل في إنشاء الحساب.");
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginRequest request)
        {
            var token = await _authService.Login(request.Email, request.Password);

            if (token == null)
                return Unauthorized(new { message = "الإيميل أو كلمة السر خطأ" });

            return Ok(new { token = token, message = "تم تسجيل الدخول بنجاح" });
        }
    }

    public class LoginRequest
    {
        public string Email { get; set; }
        public string Password { get; set; }
    }
}