using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MindEdge_1.Models
{
    public class User
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid Id { get; set; } = Guid.NewGuid();
        [Required, StringLength(100)]
        public string Name { get; set; }                                                   
        [Required, EmailAddress, StringLength(100)]
        public string Email { get; set; }
        [Required]
        public string Password { get; set; }
        public string? Image { get; set; }
        //for email verification
        public bool IsVerified { get; set; } = false;
        //for reset password
        public string? Code { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.Now;
    }

}