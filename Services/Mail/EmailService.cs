
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Options;
using Services.Mail.Models;
using System.Collections.Generic;
using System.Net;
using System.Net.Mail;
using System.Threading.Tasks;

namespace Services.Mail
{
    public class EmailService
    {
        private readonly ConnectionOptions _optionsAccessor;
        private readonly SmtpClient _smtp;

        public EmailService(IOptions<ConnectionOptions> optionsAccessor)
        {
            this._optionsAccessor = optionsAccessor.Value;
            _smtp = new SmtpClient();
        }


        public async Task Send(string to, string subject, string body)
        {
            var mail = new MailMessage();
            mail.From = new MailAddress(_optionsAccessor.UserName);
            mail.To.Add(new MailAddress(to));
            mail.Subject = subject;
            mail.IsBodyHtml = true;
            mail.Body = body;
            _smtp.Host = _optionsAccessor.Host;
            _smtp.EnableSsl = _optionsAccessor.UseSsl;
        }
    }
}
