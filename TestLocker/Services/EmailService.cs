using Microsoft.Extensions.Configuration;
using SendGrid;
using SendGrid.Helpers.Mail;
using System.Threading.Tasks;

namespace TestLocker.Services
{
    public class EmailService : IEmailService
    {
        private readonly SendGridClient _client;
        private readonly string _appName;
        private readonly string _appEmail;

        public EmailService(IConfiguration configuration)
        {
            var key = configuration["TestLocker:SendGrid:Key"];
            _client = new SendGridClient(key);

            _appName = configuration["App:Name"];
            _appEmail = configuration["App:Email"];
        }

        public async Task<int> SendEmail(string to, string subject, string body)
        {
            var from = new EmailAddress(_appEmail, _appName);
            var sendToEmail = new EmailAddress(to);

            var email = MailHelper.CreateSingleEmail(from, sendToEmail, subject, body, body);

            var response = await _client.SendEmailAsync(email);

            return response.StatusCode.GetHashCode();
        }
    }
}
