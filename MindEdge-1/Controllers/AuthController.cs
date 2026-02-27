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
        public async Task<IActionResult> Register(RegisterDto model)
        {
            var result = await _authService.RegisterAsync(model);
            return result == "Success" ? Ok(new { message = result }) : BadRequest(result);
        }

        [HttpPost("verify-email")]
        public async Task<IActionResult> VerifyEmail(string email, string code)
        {
            var result = await _authService.VerifyEmailAsync(email, code);
            return result ? Ok(new { message = "Email Verified Successfully" }) : BadRequest("Invalid Code");
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login(LoginDto model)
        {
            var result = await _authService.LoginAsync(model);
            if (result.Contains("Invalid") || result.Contains("verify"))
                return BadRequest(new { message = result });

            return Ok(new { Token = result, message = "Logged in successfully" });
        }

        [HttpPost("forgot-password")]
        public async Task<IActionResult> ForgotPassword(string email)
        {
            var result = await _authService.ForgotPasswordAsync(email);
            if (result)
                return Ok(new { message = "تم إرسال كود إعادة التعيين إلى بريدك الإلكتروني." });

            return BadRequest(new { message = "هذا البريد الإلكتروني غير مسجل لدينا." });
        }

        [HttpPost("reset-password")]
        public async Task<IActionResult> ResetPassword(ResetPasswordDto model)
        {
            var result = await _authService.ResetPasswordAsync(model);
            if (result)
                return Ok(new { message = "تم تغيير كلمة السر بنجاح!" });

            return BadRequest(new { message = "الكود غير صحيح أو الإيميل غير موجود." });
        }
    }
}