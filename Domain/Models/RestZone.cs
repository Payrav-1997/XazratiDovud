using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Domain.Models
{
    public class RestZone : BaseModel
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public bool BestRestZone { get; set; }
        public virtual ICollection<RestZoneFiles> Files { get; set; }
    }
}
