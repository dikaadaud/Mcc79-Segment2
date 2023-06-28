using API.Ultilities.Enum;
using System.ComponentModel.DataAnnotations;

namespace API.DTOs.Account
{
    public class RegisterDto
    {
        [Required]
        public string FirstName { get; set; }
        public string? LastName { get; set; }
        [Required]
        public DateTime BirthDate { get; set; }
        [Required]
        public GenderEnum Gender { get; set; }
        [Required]
        public DateTime HiringDate { get; set; }
        [Required]
        [EmailAddress]
        public string Email { get; set; }
        [Required]
        [Phone]
        public string PhoneNumber { get; set; }
        [Required]
        [PasswordPolicy]
        public string Password { get; set; }
        [Required(ErrorMessage = ("Password not Match"))]
        [PasswordPolicy]
        [DataType(DataType.Password)]
        [Compare("Password")]
        public string ConfirmPassword { get; set; }
        [Required]
        public string Major { get; set; }
        [Required]
        public string Degree { get; set; }
        [Required]
        [Range(0, 4, ErrorMessage = "Gpa Must 0 - 4")]
        public float GPA { get; set; }
        [Required]
        public string UniversityCode { get; set; }
        [Required]
        public string UniversityName { get; set; }
    }

}
