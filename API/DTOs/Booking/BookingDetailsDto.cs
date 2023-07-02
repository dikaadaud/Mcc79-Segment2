using API.Ultilities.Enum;

namespace API.DTOs.Booking
{
    public class BookingDetailsDto
    {
        public Guid BookingGuid { get; set; }
        public string BookedBy { get; set; }
        public StatusLevel Status { get; set; }
        public int Floor { get; set; }
        public string RoomName { get; set; }

    }
}
