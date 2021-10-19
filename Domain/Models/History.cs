using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Project.Models
{
    public class History : BaseModel
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public IFormFile HisFile { get; set; }
    }
}
