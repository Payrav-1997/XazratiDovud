using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Domain.Models
{
    public class RestZoneFiles : BaseModel
    {
        public string Name { get; set; }
        public int RestZoneId { get; set; }
        [ForeignKey("RestZoneId")]
        public virtual RestZone RestZone { get; set; }
    }
}
