using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Domain.Models
{
    public class BaseModel
    {
        public DateTime CreateDate { get; set; }
        public DateTime UpdateDate { get; set; }

    }
}
