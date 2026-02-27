using Azure;
using System.ComponentModel.DataAnnotations;

namespace MindEdge_1.Models
{
    public class Message
    {
        [Key]
        public Guid Id { get; set; } = Guid.NewGuid();

        [Required]
        public string Content { get; set; } 

        public string Sender { get; set; }

        public DateTime SentAt { get; set; } = DateTime.Now;

        public Guid ChatbotRoomId { get; set; }
        public ChatbotRoom ChatbotRoom { get; set; }
        public AIResponse AIResponse { get; set; }
    }
}
