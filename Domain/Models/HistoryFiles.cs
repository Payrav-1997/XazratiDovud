using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Models
{
    public class HistoryFiles : BaseModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string FileSize { get; set; }
        public int HistoryId { get; set; }
        [ForeignKey("HistoryId")]
        public virtual History History { get; set; }
    }
}