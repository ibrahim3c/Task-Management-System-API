using AutoMapper.Internal;
using MailKit.Net.Smtp;
using MailKit.Security;
using Microsoft.Extensions.Options;
using MimeKit;
using SendGrid.Helpers.Mail;
using SendGrid;
using Task_Management_System_API.Helpers;
using Task_Management_System_API.Services.Interfaces;
using Core.DTOS;

namespace Task_Management_System_API.Services.Implementations
{
    public class MaillingService : IMaillingService
    {
        private readonly IOptionsMonitor<MaillingSettings> mailSettings;
        private readonly IOptionsMonitor<SendGridSettings> sendGridSettigns;

        public MaillingService(IOptionsMonitor<MaillingSettings> mailSettings,IOptionsMonitor<SendGridSettings> sendGridSettigns)
        {
            this.mailSettings = mailSettings;
            this.sendGridSettigns = sendGridSettigns;
        }

        // it does not work
        public async Task SendMailAsync(string mailTo, string subject, string body, IList<IFormFile> files = null)
        {
            var email = new MimeMessage
            {
                Sender = MailboxAddress.Parse(mailSettings.CurrentValue.SenderMail),
                Subject = subject
            };
            email.To.Add(MailboxAddress.Parse(mailTo));
            var builder = new BodyBuilder();
            if (files != null)
            {
                byte[] fileBytes;
                foreach (var file in files)
                {
                    if (file.Length > 0)
                    {
                        using var mo = new MemoryStream();
                        file.CopyTo(mo);
                        fileBytes = mo.ToArray();

                        builder.Attachments.Add(file.FileName, fileBytes, ContentType.Parse(file.ContentType));


                    }

                }
            }

            builder.HtmlBody = body;
            email.Body=builder.ToMessageBody();
            email.From.Add(new MailboxAddress(mailSettings.CurrentValue.DisplayName, mailSettings.CurrentValue.SenderMail));


            var smtp = new SmtpClient();
            smtp.Connect(mailSettings.CurrentValue.Host, mailSettings.CurrentValue.Port, SecureSocketOptions.StartTls);
            smtp.Authenticate(mailSettings.CurrentValue.SenderMail, mailSettings.CurrentValue.Password);
            await smtp.SendAsync(email);
            smtp.Disconnect(true);
        }

        // it works :)
        // using sendGrid
        public async Task SendMailBySendGridAsync(MailRequestDTO mailRequest)
        {
            var client = new SendGridClient(sendGridSettigns.CurrentValue.ApiKey);
            var from = new EmailAddress(sendGridSettigns.CurrentValue.SenderMail,sendGridSettigns.CurrentValue.SenderName);
            var to = new EmailAddress(mailRequest.ToEmail);

            var msg = MailHelper.CreateSingleEmail(from, to, mailRequest.Subject, mailRequest.Body, mailRequest.Body);

            // Attach files if any
            if (mailRequest.Attachments != null && mailRequest.Attachments.Count > 0)
            {
                foreach (var attachment in mailRequest.Attachments)
                {
                    using (var ms = new MemoryStream())
                    {
                        await attachment.CopyToAsync(ms);
                        var fileBytes = ms.ToArray();
                        msg.AddAttachment(attachment.FileName, Convert.ToBase64String(fileBytes));
                    }
                }
            }

            var response = await client.SendEmailAsync(msg);

            if (!response.IsSuccessStatusCode)
            {
                throw new Exception($"Failed to send email: {response.StatusCode}");
            }

        }
        public async Task SendMailBySendGridAsync(string mailTo, string subject, string body, IList<IFormFile> files = null)
        {
            var client = new SendGridClient(sendGridSettigns.CurrentValue.ApiKey);
            var from = new EmailAddress(sendGridSettigns.CurrentValue.SenderMail, sendGridSettigns.CurrentValue.SenderName);
            var to = new EmailAddress(mailTo);

            var msg = MailHelper.CreateSingleEmail(from, to, subject, body, body);

            // Attach files if any
            if (files != null && files.Count > 0)
            {
                foreach (var file in files)
                {
                    using var ms = new MemoryStream();
                    await file.CopyToAsync(ms);
                    var fileBytes = ms.ToArray();
                    msg.AddAttachment(file.FileName, Convert.ToBase64String(fileBytes));
                }
            }

            // Send email and handle response
            var response = await client.SendEmailAsync(msg);

            if (!response.IsSuccessStatusCode)
            {
                var errorMessage = await response.Body.ReadAsStringAsync();
                throw new Exception($"Failed to send email: {response.StatusCode} - {errorMessage}");
            }
        }
    }
}