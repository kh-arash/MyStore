using MailKit.Net.Smtp;
using MimeKit;
using MyStore.Service.Constants;
using MyStore.Service.Models;

namespace MyStore.Service.Services.Email
{
    public class EmailService : IEmailService
    {
        private readonly EmailConfig _emailConfig;
        public EmailService(EmailConfig emailConfig)
        {
            _emailConfig = emailConfig;
        }

        public async Task<string> SendEmail(Message message)
        {
            var emailMessage = CreateEmailMessage(message);
            await Send(emailMessage);
            var recipients = string.Join(", ", message.To);
            return ResponseMessages.GetEmailSuccessMessage(recipients);
        }

        private async Task Send(MimeMessage emailMessage)
        {
            using var client = new SmtpClient();
            try
            {
                await client.ConnectAsync(_emailConfig.SmtpServer, _emailConfig.Port, true);
                client.AuthenticationMechanisms.Remove("XOAUTH2");
                await client.AuthenticateAsync(_emailConfig.Username, _emailConfig.Password);

                await client.SendAsync(emailMessage);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                await client.DisconnectAsync(true);
                client.Dispose();
            }
        }

        private MimeMessage CreateEmailMessage(Message message)
        {
            var emailMessage = new MimeMessage();
            emailMessage.From.Add(new MailboxAddress("myStore", _emailConfig.From));
            emailMessage.To.AddRange(message.To);
            emailMessage.Subject = message.Subject;
            emailMessage.Body = new TextPart(MimeKit.Text.TextFormat.Text) { Text = message.Body };

            return emailMessage;
        }
    }
}
