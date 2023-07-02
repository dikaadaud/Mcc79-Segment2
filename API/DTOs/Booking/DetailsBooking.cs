using API.Ultilities.Enum;
using System.ComponentModel.DataAnnotations;

namespace API.DTOs.Booking
{
    public class DetailsBooking
    {
        [Required]
        public Guid Guid { get; set; }
        [Required]
        public string BookedNik { get; set; }
        [Required]
        public string BookedBy { get; set; }
        [Required]
        public string RoomName { get; set; }
        [Required]
        public DateTime StartDate { get; set; }
        [Required]
        public DateTime EndDate { get; set; }
        [Required]
        public StatusLevel Status { get; set; }
        [Required]
        public string Remarks { get; set; }
    }
}
