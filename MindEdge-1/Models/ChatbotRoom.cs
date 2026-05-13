using System.ComponentModel.DataAnnotations;

namespace MindEdge_1.Models
{
    public class ChatbotRoom
    {
        [Key]
        public string SessionId { get; set; }
        public string FileName { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.Now;
    }
}