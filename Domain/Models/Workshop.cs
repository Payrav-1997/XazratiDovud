using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Project.Models
{
    public class Workshop : BaseModel
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public IFormFile WFile { get; set; }
    }
}
