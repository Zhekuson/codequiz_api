using MailKit.Net.Smtp;
using MimeKit;
using Services.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

using System.Threading.Tasks;

namespace Services.Services.Classes
{
    public class MailService:IMailService
    {
        public async Task SendEmailAsync(string email, string subject, string message)
        {
            var emailMessage = new MimeMessage();

            emailMessage.From.Add(new MailboxAddress(MailOptions.mailTitle, MailOptions.mailLogin));
            emailMessage.To.Add(new MailboxAddress("", email));
            emailMessage.Subject = subject;
            emailMessage.Body = new TextPart(MimeKit.Text.TextFormat.Html)
            {
                Text = message
            };

            using (var client = new SmtpClient())
            {
                await client.ConnectAsync(MailOptions.server, 25, false);
                await client.AuthenticateAsync(MailOptions.mailLogin, MailOptions.password);
                await client.SendAsync(emailMessage);

                await client.DisconnectAsync(true);
            }
        }
    }
}
