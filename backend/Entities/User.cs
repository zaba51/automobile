using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace backend.Entities
{
    public class User
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id {get; set; }
        public string Email {get; set; }
        public string Role { get; set; }

        [Required]
        [MaxLength(50)]
        public string Password { get; set; }

        
        public User()
        {
            Role = "user";
        }
    }
}