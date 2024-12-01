
using Microsoft.AspNetCore.DataProtection.KeyManagement;
using SendGrid.Helpers.Mail;
using SendGrid;

namespace Hangfire.Web.Services
{
    public class EmailSender : IEmailSender
    {
        private readonly IConfiguration _configuration;
        public EmailSender(IConfiguration configuration)
        {
            _configuration = configuration;
        }


        public async Task SendEmailAsync(string email, string subject, string message)
        {
            var apiKey = _configuration["APIs:SendGridApiKey"];

            var senderMail = "admin@anilorhan.dev";

            var senderName = "Anıl Orhan";

            var client = new SendGridClient(apiKey);

            var from = new EmailAddress(senderMail, senderName);

            var subjectText = subject;

            var to = new EmailAddress(email, "Example User");

            var plainTextContent = message;

            var htmlContent = "<strong>and easy to do anywhere, even with C#</strong>";

            var msg = MailHelper.CreateSingleEmail(from, to, subject, plainTextContent, htmlContent);

            var response = await client.SendEmailAsync(msg);
        }
    }
}
