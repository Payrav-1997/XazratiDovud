using System;

namespace Services.Hostory.Models
{
    public class UpdateHistoryViewModel
    {
        public int Id { get; set; }          
        public string Title { get; set; }
        public string Description { get; set; }
        public DateTime UpdateDate { get; set; }
    }
}