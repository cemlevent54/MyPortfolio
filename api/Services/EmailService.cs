using api.DTOs.Email;
using System.Net.Mail;
using System.Threading.Tasks;
using MailKit.Net.Smtp;
//MimeKit
using MimeKit;
//smtpclient
using MailKit.Security;
using SmtpClient = MailKit.Net.Smtp.SmtpClient;

namespace api.Services
{
    public class EmailService
    {
        private readonly IConfiguration _configuration;

        public EmailService(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public async Task<bool> SendEmailAsync(EmailRequest request)
        {
            try
            {
                var email = new MimeMessage();
                email.From.Add(new MailboxAddress("Your Website", _configuration["Email:User"]));
                email.To.Add(new MailboxAddress("Admin", _configuration["Email:Receiver"]));
                email.Subject = request.Subject;

                email.Body = new TextPart("plain")
                {
                    Text = $"Name: {request.Name}\nEmail: {request.Email}\n\nMessage:\n{request.Message}"
                };

                using var smtp = new SmtpClient();
                await smtp.ConnectAsync(_configuration["Email:SmtpServer"], int.Parse(_configuration["Email:Port"]), false);
                await smtp.AuthenticateAsync(_configuration["Email:User"], _configuration["Email:Password"]);
                await smtp.SendAsync(email);
                await smtp.DisconnectAsync(true);

                return true;
            }
            catch
            {
                return false;
            }
        }
    }
}
