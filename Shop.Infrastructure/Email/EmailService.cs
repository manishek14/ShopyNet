using Common.Application.Email; 
using MailKit.Net.Smtp;
using MailKit.Security;
using Microsoft.Extensions.Options;
using MimeKit;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Shop.Infrastructure.Email
{
    public class EmailService : IEmailService
    {
        private readonly EmailSettings _settings;

        public EmailService(IOptions<EmailSettings> settings)
        {
            _settings = settings.Value;
        }

        public async Task SendEmailAsync(
            string to,
            string subject,
            string body,
            CancellationToken cancellationToken = default)
        {
            var email = new MimeMessage();
            email.From.Add(MailboxAddress.Parse(_settings.From));
            email.To.Add(MailboxAddress.Parse(to));
            email.Subject = subject;

            var builder = new BodyBuilder { HtmlBody = body };
            email.Body = builder.ToMessageBody();

            using var smtp = new MailKit.Net.Smtp.SmtpClient();

            await smtp.ConnectAsync(
                _settings.SmtpHost,
                _settings.SmtpPort,
                _settings.EnableSsl ? SecureSocketOptions.StartTls : SecureSocketOptions.None,
                cancellationToken);

            await smtp.AuthenticateAsync(_settings.Username, _settings.Password, cancellationToken);
            await smtp.SendAsync(email, cancellationToken);
            await smtp.DisconnectAsync(true, cancellationToken);
        }
    }
}