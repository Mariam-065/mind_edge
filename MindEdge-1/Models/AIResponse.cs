using System.ComponentModel.DataAnnotations;

namespace MindEdge_1.Models
{
    public class AIResponse
    {
        [Key]
        public Guid Id { get; set; } = Guid.NewGuid();

        [Required]
        public string ResponseText { get; set; }


        public string? RetrievedContext { get; set; }    //RAG

        public string? DetectedIntent { get; set; }      

        public DateTime CreatedAt { get; set; } = DateTime.Now;

        public Guid MessageId { get; set; }
        public Message Message { get; set; }
    }
}
