using API.Ultilities.Enum;
using System.ComponentModel.DataAnnotations;

namespace API.DTOs.Account
{
    public class NewAccountDto
    {
        [Required(ErrorMessage = "Harus Masukan Guid")]
        public Guid Guid { get; set; }
        [Required(ErrorMessage = "Harus masukan Password")]
        [PasswordPolicy]
        public string Password { get; set; }
        [Required]
        public bool IsDeleted { get; set; }
        [Required]
        public int Otp { get; set; }
        [Required]
        public bool IsUsed { get; set; }
        [Required]
        public DateTime ExpiredTime { get; set; }
    }
}
