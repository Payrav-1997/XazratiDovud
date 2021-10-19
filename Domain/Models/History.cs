using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Domain.Models
{
    public class History : BaseModel
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public string HisFile { get; set; }
    }
}
