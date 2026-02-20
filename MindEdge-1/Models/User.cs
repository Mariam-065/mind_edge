using System.ComponentModel.DataAnnotations;

namespace MindEdge_1.Models
{
    public class User
    {
        [Key] 
        public string Id { get; set; } = Guid.NewGuid().ToString();

        [Required, StringLength(100)]
        public string Name { get; set; }

        [Required, EmailAddress, StringLength(100)]
        public string Email { get; set; }

        [Required]
        public string Password { get; set; } 

        public string Role { get; set; } 

        public string Image { get; set; }

        public bool IsVerified { get; set; } = false; 

        public string Code { get; set; } 

        public DateTime CreatedAt { get; set; } = DateTime.Now;
    }
}