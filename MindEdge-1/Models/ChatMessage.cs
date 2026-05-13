
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    namespace MindEdge_1.Models
    {
        public class ChatMessage
            {
                public int Id { get; set; }
                public string SessionId { get; set; } = string.Empty;
                public string Role { get; set; } = string.Empty; 
                public string Content { get; set; } = string.Empty;
                public DateTime SentAt { get; set; } = DateTime.Now;
                public string? UserId { get; set; } 
            }
    }

