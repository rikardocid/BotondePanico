using GFDSystems.Vigitech.DAO.Tools.Settings;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Threading.Tasks;

namespace GFDSystems.Vigitech.DAO.Tools.Email
{
    public class EmailSender : IEmailSender
    {
        private readonly EmailSettings _emailSettings;

        public EmailSender(IOptions<EmailSettings> emailSettings)
        {
            _emailSettings = emailSettings.Value;
        }

        public async Task SendEmailAsync(string emailSend, string Body, string Asunto = "")
        {
            SmtpClient smtpClient = new SmtpClient();
            smtpClient.Host = _emailSettings.SmtpServer;
            smtpClient.EnableSsl = _emailSettings.SmtpServerEnabledSsl;
            smtpClient.Port = _emailSettings.SmtpPort;
            smtpClient.Credentials = new NetworkCredential(_emailSettings.Account, _emailSettings.Password);
            //cuautlancingo
            MailMessage mailMessage = new MailMessage();
            mailMessage.To.Add(emailSend);
            mailMessage.From = new MailAddress(_emailSettings.SenderEmail, _emailSettings.SenderName);
            mailMessage.Subject = Asunto;
            mailMessage.Body = Body;
            mailMessage.IsBodyHtml = true;
            mailMessage.Priority = MailPriority.High;

            await smtpClient.SendMailAsync(mailMessage);
            smtpClient.Dispose();
        }
    }
}
