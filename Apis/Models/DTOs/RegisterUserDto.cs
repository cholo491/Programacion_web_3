using System.ComponentModel.DataAnnotations;

namespace Apis.Models.DTOs
{
    public class RegisterUserDto
    {
        [Required, MaxLength(100)]
        public string Name { get; set; }
        [Required, EmailAddress]
        public string Email { get; set; }
        [Required]
        public string Password { get; set; }
        [Required]
        public int Role { get; set; } 
    }
}
