
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Options;
using System.Collections.Generic;
using System.Net;
using System.Net.Mail;
using System.Threading.Tasks;

namespace Services.Mail
{
    public class EmailService
    {
        public EmailService(IOptions<ConnectionOptions> options)
        {
        }
    }
}
