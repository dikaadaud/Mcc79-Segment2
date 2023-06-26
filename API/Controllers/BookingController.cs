using API.Contracts;
using API.Models;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [ApiController]
    [Route("api/Bookings")]
    public class BookingController : GeneralController<Booking>
    {
        public BookingController(IBookingRepository bookingRepository) : base(bookingRepository)
        {
        }
    }
}
