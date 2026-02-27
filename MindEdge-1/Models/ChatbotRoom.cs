using System.ComponentModel.DataAnnotations;

namespace MindEdge_1.Models
{
    public class ChatbotRoom
    {
        [Key]
        public Guid Id { get; set; } = Guid.NewGuid();

        [Required, StringLength(100)]
        public string Title { get; set; }

        public DateTime CreatedAt { get; set; } = DateTime.Now;

        public Guid UserId { get; set; }
        public User User { get; set; }

        public ICollection<Message> Messages { get; set; }
    }
}
