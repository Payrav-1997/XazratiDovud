using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Models
{
    public class User : IdentityUser<int>
    {
        public bool Active { get; set; }
        public DateTime BirthDate { get; set; }
        public DateTime Updated { get; set; }
        public DateTime Created { get; set; }
    }
}
