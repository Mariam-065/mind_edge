using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using MindEdge_1.Data;
using MindEdge_1.Models;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using Microsoft.Extensions.Caching.Distributed; // مكتبة الريديس
using System.Text.Json; // للتعامل مع الـ JSON

namespace MindEdge_1.Services
{
    public class AuthService : IAuthService
    {
        private readonly ApplicationDbContext _context;
        private readonly IConfiguration _configuration;
        private readonly IEmailService _emailService;
        private readonly IDistributedCache _cache; // إضافة الـ Cache

        public AuthService(ApplicationDbContext context, IConfiguration configuration, IEmailService emailService, IDistributedCache cache)
        {
            _context = context;
            _configuration = configuration;
            _emailService = emailService;
            _cache = cache;
        }

        public async Task<string> RegisterAsync(RegisterDto model)
        {
            // 1. التأكد إن المستخدم مش موجود أصلاً في الداتابيز الحقيقية
            if (await _context.Users.AnyAsync(u => u.Email == model.Email))
                return "User already exists!";

            // 2. توليد كود التحقق
            var verificationCode = RandomNumberGenerator.GetInt32(100000, 999999).ToString();

            // 3. تشفير الباسورد قبل التخزين في الريديس
            var hashedPassword = BCrypt.Net.BCrypt.HashPassword(model.Password);

            // 4. تجهيز كائن مؤقت للمستخدم
            var tempUser = new User
            {
                Name = model.Name,
                Email = model.Email,
                Password = hashedPassword,
                Code = verificationCode,
                IsVerified = false
            };

            // 5. تخزين المستخدم في Redis لمدة 15 دقيقة
            var cacheOptions = new DistributedCacheEntryOptions
            {
                AbsoluteExpirationRelativeToNow = TimeSpan.FromMinutes(15)
            };
            var userJson = JsonSerializer.Serialize(tempUser);
            await _cache.SetStringAsync($"temp_user_{model.Email}", userJson, cacheOptions);

            // 6. إرسال الإيميل (بالشكل الاحترافي اللي عملناه)
            await _emailService.SendEmailAsync(model.Email, "MindEdge - Verify Your Email", verificationCode);

            return "Success";
        }

        public async Task<bool> VerifyEmailAsync(string email, string code)
        {
            // 1. البحث عن المستخدم في Redis
            var userJson = await _cache.GetStringAsync($"temp_user_{email}");
            if (userJson == null) return false; // الوقت خلص أو الإيميل غلط

            var user = JsonSerializer.Deserialize<User>(userJson);

            // 2. التأكد من الكود
            if (user.Code != code) return false;

            // 3. الكود صح؟ ننقل المستخدم للداتابيز الحقيقية
            user.IsVerified = true;
            user.Code = null;

            _context.Users.Add(user);
            await _context.SaveChangesAsync();

            // 4. حذف البيانات من Redis
            await _cache.RemoveAsync($"temp_user_{email}");

            return true;
        }

        public async Task<string> LoginAsync(LoginDto model)
        {
            var user = await _context.Users.FirstOrDefaultAsync(u => u.Email == model.Email);
            if (user == null || !BCrypt.Net.BCrypt.Verify(model.Password, user.Password))
                return "Invalid email or password.";

            if (!user.IsVerified) return "Please verify your email first.";

            return CreateToken(user);
        }

        public async Task<bool> ForgotPasswordAsync(string email)
        {
            var user = await _context.Users.FirstOrDefaultAsync(u => u.Email == email);
            if (user == null) return false;

            user.Code = RandomNumberGenerator.GetInt32(100000, 999999).ToString();
            await _context.SaveChangesAsync();

            await _emailService.SendEmailAsync(user.Email, "MindEdge - Reset Password", $"Your reset code is: {user.Code}");
            return true;
        }

        public async Task<bool> ResetPasswordAsync(ResetPasswordDto model)
        {
            var user = await _context.Users.FirstOrDefaultAsync(u => u.Email == model.Email);
            if (user == null || user.Code != model.Code) return false;

            user.Password = BCrypt.Net.BCrypt.HashPassword(model.NewPassword);
            user.Code = null;
            await _context.SaveChangesAsync();
            return true;
        }

        private string CreateToken(User user)
        {
            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                new Claim(ClaimTypes.Name, user.Name),
                new Claim(ClaimTypes.Email, user.Email)
            };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                issuer: _configuration["Jwt:Issuer"],
                audience: _configuration["Jwt:Audience"],
                claims: claims,
                expires: DateTime.Now.AddDays(7),
                signingCredentials: creds
            );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}