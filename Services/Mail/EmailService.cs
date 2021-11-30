
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
            _smtp.Credentials = new NetworkCredential(_optionsAccessor.UserName, _optionsAccessor.Password);
            await _smtp.SendMailAsync(mail);
        }

        public async Task Send(string to, string subject, string body, IFormFile file)
        {
            var mail = new MailMessage();

            mail.From = new MailAddress(_optionsAccessor.UserName);
            mail.To.Add(new MailAddress(to));
            mail.Subject = subject;
            mail.Body = body;
            if (file != null)
            {
                mail.Attachments.Add(new Attachment(file.OpenReadStream(), file.FileName, file.ContentType));
            }

            _smtp.Host = _optionsAccessor.Host;
            _smtp.EnableSsl = _optionsAccessor.UseSsl;
            _smtp.Credentials = new NetworkCredential(_optionsAccessor.UserName, _optionsAccessor.Password);
            await _smtp.SendMailAsync(mail);
        }

        public async Task Send(List<string>to,string subject,string body)
        {
            var mail = new MailMessage();
            mail.From = new MailAddress(_optionsAccessor.UserName);

            to.ForEach(x =>
            {
                mail.To.Add(x);
            });
            mail.Subject = subject;
            mail.IsBodyHtml = true;
            mail.Body = body;

            _smtp.Host = _optionsAccessor.Host;
            _smtp.EnableSsl = _optionsAccessor.UseSsl;
            _smtp.Credentials = new NetworkCredential(_optionsAccessor.UserName, _optionsAccessor.Password);
            await _smtp.SendMailAsync(mail);
        }


    }
}
