using System;
using Microsoft.AspNetCore.Http;

namespace Services.Hostory.Models
{
    public class HistoryViewModel
    {
        public string Title { get; set; }
        public string Description { get; set; }
        
        public DateTime CreateDate { get; set; }
        
        public IFormFile[] Files { get; set; }
    }
}