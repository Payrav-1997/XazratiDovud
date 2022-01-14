using System.Collections.Generic;

namespace Services.Hostory.Models
{
    public class GetHistoryViewModel
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public List<string> Images { get; set; }
    }
}