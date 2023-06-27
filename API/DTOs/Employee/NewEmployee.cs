using API.Ultilities.Enum;

namespace API.DTOs.Employee
{
    public class NewEmployee
    {
        public string Nik { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateTime BirthDate { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public DateTime HiringDate { get; set; }
        public GenderEnum Gender { get; set; }
    }
}
