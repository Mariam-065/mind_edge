using MailKit.Net.Smtp;
using Microsoft.Extensions.Configuration;
using MimeKit;
using MindEdge_1.Templates;

namespace MindEdge_1.Services
{
    public class EmailService : IEmailService
    {
        private readonly IConfiguration _configuration;

        public EmailService(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public async Task SendEmailAsync(string email, string subject, string message)
        {
            // 1. سحب البيانات من appsettings.json
            var senderName = _configuration["EmailSettings:SenderName"];
            var senderEmail = _configuration["EmailSettings:SenderEmail"];
            var userName = _configuration["EmailSettings:UserName"];
            var appPassword = _configuration["EmailSettings:AppPassword"];

            // 2. تجهيز الرسالة باستخدام MimeKit
            var emailMessage = new MimeMessage();
            emailMessage.From.Add(new MailboxAddress(senderName, senderEmail));
            emailMessage.To.Add(new MailboxAddress("", email));
            emailMessage.Subject = subject;

            string messageBody = EmailTemplates.GetVerificationCodeTemplate(message);

            emailMessage.Body = new TextPart("html") { Text = messageBody };

            // 3. الإرسال باستخدام MailKit
            using var client = new SmtpClient();
            try
            {
                // الاتصال بسيرفر بريفو
                await client.ConnectAsync("smtp-relay.brevo.com", 587, MailKit.Security.SecureSocketOptions.StartTls);

                // تسجيل الدخول (Username هو الكود اللى بيبدأ بـ a92..)
                await client.AuthenticateAsync(userName, appPassword);

                // الإرسال الفعلي
                await client.SendAsync(emailMessage);

                await client.DisconnectAsync(true);
            }
            catch (Exception ex)
            {
                // طباعة الخطأ لو حصل مشكلة في السيرفر
                Console.WriteLine($"MailKit Error: {ex.Message}");
                throw;
            }
        }
    }
}