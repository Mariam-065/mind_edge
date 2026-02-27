using System.Net;
using System.Net.Mail;

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
            // يفضل سحب هذه البيانات من appsettings.json للأمان
            var smtpServer = "smtp.gmail.com";
            var port = 587;
            var senderEmail = _configuration["EmailSettings:SenderEmail"];
            var appPassword = _configuration["EmailSettings:AppPassword"];

            using var client = new SmtpClient(smtpServer, port)
            {
                EnableSsl = true,
                Credentials = new NetworkCredential(senderEmail, appPassword)
            };

            var mailMessage = new MailMessage(senderEmail, email, subject, message);
            await client.SendMailAsync(mailMessage);
        }
    }
}