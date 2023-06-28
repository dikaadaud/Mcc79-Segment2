using System.ComponentModel.DataAnnotations;

namespace API.DTOs.Education
{
    public class NewEducationDto
    {
        [Required]
        public Guid Guid { get; set; }

        [Required]
        public string Major { get; set; }

        [Required]
        public string Degree { get; set; }

        [Required]
        public float Gpa { get; set; }

        [Required]
        public Guid UniversityGuid { get; set; }
    }
}
