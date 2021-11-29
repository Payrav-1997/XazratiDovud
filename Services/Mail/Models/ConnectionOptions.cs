using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Mail.Models
{
    public class ConnectionOptions
    {
        public string Host { get; set; }
        public UInt16 Port { get; set; }
        public bool UseSsl { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
    }
}
