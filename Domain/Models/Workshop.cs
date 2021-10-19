using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
namespace Domain.Models
{
    public class Workshop : BaseModel
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public string WFile { get; set; }
    }
}
