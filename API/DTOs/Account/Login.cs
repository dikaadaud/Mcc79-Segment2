using API.Ultilities.Enum;
using System.ComponentModel.DataAnnotations;

namespace API.DTOs.Account
{
    public class Login
    {
        [Required]
        [EmailAddress]
        public string Email { get; set; }
        [Required]
        [PasswordPolicy]
        public string Password { get; set; }
    }
}
