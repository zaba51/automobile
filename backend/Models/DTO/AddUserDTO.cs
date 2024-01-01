using System.ComponentModel.DataAnnotations;
using backend.Entities;

namespace backend.Models
{
    public class AddUserDTO
    {
        [Required]
        public string Email { get; set; }
        [Required]
        public string Password { get; set; }
    }
}