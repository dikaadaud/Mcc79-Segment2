using API.Contracts;
using API.Models;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [ApiController]
    [Route("api/Bookings")]
    public class BookingController : ControllerBase
    {
        private readonly IBookingRepository _bookingRepository;

        public BookingController(IBookingRepository bookingRepository)
        {
            _bookingRepository = bookingRepository;
        }

        [HttpGet]
        public IActionResult GetAll()
        {
            var booking = _bookingRepository.GetAll();
            if (booking == null)
            {
                return NotFound();
            }
            return Ok(booking);
        }

        [HttpGet("{guid}")]
        public IActionResult GetByGuid(Guid guid)
        {
            var booking = _bookingRepository.GetByGuid(guid);
            if (booking == null)
            {
                return NotFound();
            }
            return Ok(booking);
        }

        [HttpPost]
        public IActionResult Create(Booking bookings)
        {
            var booking = _bookingRepository.Create(bookings);
            if (booking == null)
            {
                return NotFound();
            }
            return Ok(booking);
        }

        [HttpPut]
        public IActionResult Update(Booking bookings)
        {
            var booking = _bookingRepository.Update(bookings);
            if (!booking)
            {
                return NotFound();
            }
            return Ok();
        }

        [HttpDelete]
        public IActionResult Delete(Guid guid)
        {
            var booking = _bookingRepository.Delete(guid);
            if (!booking)
            {
                return NotFound();
            }
            return Ok();
        }
    }
}
