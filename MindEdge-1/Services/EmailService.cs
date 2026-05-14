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
            var senderName = _configuration["EmailSettings:SenderName"];
            var senderEmail = _configuration["EmailSettings:SenderEmail"];
            var userName = _configuration["EmailSettings:UserName"];
            var appPassword = _configuration["EmailSettings:AppPassword"];

            var emailMessage = new MimeMessage();
            emailMessage.From.Add(new MailboxAddress(senderName, senderEmail));
            emailMessage.To.Add(new MailboxAddress("", email));
            emailMessage.Subject = subject;

            string messageBody = EmailTemplates.GetVerificationCodeTemplate(message);

            emailMessage.Body = new TextPart("html") { Text = messageBody };

            using var client = new SmtpClient();
            try
            {
                await client.ConnectAsync("smtp-relay.brevo.com", 587, MailKit.Security.SecureSocketOptions.StartTls);

                await client.AuthenticateAsync(userName, appPassword);

                await client.SendAsync(emailMessage);

                await client.DisconnectAsync(true);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"MailKit Error: {ex.Message}");
                throw;
            }
        }
    }
}