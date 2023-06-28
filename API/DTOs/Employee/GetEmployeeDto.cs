﻿using API.Ultilities.Enum;
using System.ComponentModel.DataAnnotations;

namespace API.DTOs.Employee
{
    public class GetEmployeeDto
    {
        [Required]
        public Guid Guid { get; set; }
        public string Nik { get; set; }
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

    }

}
