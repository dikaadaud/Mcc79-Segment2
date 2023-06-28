﻿using API.Ultilities.Enum;
using System.ComponentModel.DataAnnotations;

namespace API.DTOs.Booking
{
    public class GetBookingDto
    {
        [Required]
        public Guid Guid { get; set; }

        [Required]
        public DateTime StartDate { get; set; }

        [Required]
        public DateTime EndDate { get; set; }

        [Required]
        public StatusLevel Status { get; set; }

        [Required]
        public string Remarks { get; set; }

        [Required]
        public Guid RoomGuid { get; set; }

        [Required]
        public Guid EmployeeGuid { get; set; }
    }
}
