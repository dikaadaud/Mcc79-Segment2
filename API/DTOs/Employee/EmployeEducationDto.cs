using API.Ultilities.Enum;

namespace API.DTOs.Employee
{
    public class EmployeEducationDto
    {
        public Guid Guid { get; set; }
        public string Nik { get; set; }
        public string Fullname { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public DateTime BirthDate { get; set; }
        public GenderEnum Gender { get; set; }
        public DateTime HiringDate { get; set; }
        public string Major { get; set; }
        public string Degree { get; set; }
        public float Gpa { get; set; }
        public string UniversityName { get; set; }

    }
}
