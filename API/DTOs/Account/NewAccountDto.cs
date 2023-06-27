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
        public bool IsDeleted { get; set; }
        public int Otp { get; set; }
        public bool IsUsed { get; set; }
        [Required]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}")]
        public DateTime ExpiredTime { get; set; }
    }
}
