using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace MindEdge_1.Models
{
    public class Document
    {
        [Key]
        public Guid Id { get; set; } = Guid.NewGuid();

        [Required]
        public string Name { get; set; }

        public string FileUrl { get; set; } //on server

        [Column(TypeName = "nvarchar(max)")]
        public string? CapturedText { get; set; } //for OCR

        public string? SummaryText { get; set; }

        public DateTime CreatedAt { get; set; } = DateTime.Now;

        //Navigation property
        public Guid UserId { get; set; }
        public User User { get; set; }
    }
}
