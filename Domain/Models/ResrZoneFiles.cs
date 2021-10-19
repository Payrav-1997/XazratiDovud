using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Project.Models
{
    public class ResrZoneFiles : BaseModel
    {
        public string Name { get; set; }
        public int MyProperty { get; set; }
        public virtual RestZone RestZone { get; set; }
    }
}
