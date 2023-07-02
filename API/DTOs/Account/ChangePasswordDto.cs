using API.Ultilities.Handler;
using System.ComponentModel.DataAnnotations;

namespace API.DTOs.Account
{
    public class ChangePasswordDto
    {
        [Required]
        [EmailAddress]
        public string Email { get; set; }
        [Required]
        public int Otp { get; set; }
        [Required]
        public string NewPassword { get; set; }
        [ConfirmPassword("NewPassword")]
        public string ConfirmPassword { get; set; }
    }
}
