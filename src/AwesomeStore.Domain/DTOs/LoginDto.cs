using System.ComponentModel.DataAnnotations;

namespace AwesomeStore.Domain.DTOs
{
    public class LoginDto
    {
        [Required(ErrorMessage = "Email is mandatory.")]
        [EmailAddress(ErrorMessage = "Email is invalid.")]
        [StringLength(100, ErrorMessage = "Email must be 100 max characteres.")]
        public string Email { get; set; }
        [Required(ErrorMessage = "Password is mandatory.")]
        [StringLength(100, ErrorMessage = "Password must be 100 max characteres.")]
        public string Password { get; set; }
    }
}