using API.Ultilities.Enum;
using System.ComponentModel.DataAnnotations;

namespace API.DTOs.Employee
{
    public class NewEmployee
    {
        //[Required(ErrorMessage = "Harus Masukan Nik")]
        //public string Nik { get; set; }

        [Required(ErrorMessage = "Harus Masukan FirstName")]
        public string FirstName { get; set; }
        public string? LastName { get; set; }

        [Required(ErrorMessage = "Harus Masukan Birthdate")]
        public DateTime BirthDate { get; set; }
        [Required]
        [EmailAddress]
        public string Email { get; set; }
        [Required]
        [Phone]
        public string PhoneNumber { get; set; }
        public DateTime HiringDate { get; set; }
        [Required]
        public GenderEnum Gender { get; set; }

    }
}
