using API.DTOs.Booking;
using API.Services;
using API.Ultilities.Handler;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace API.Controllers
{
    [ApiController]
    [Route("api/Bookings")]
    public class BookingController : ControllerBase
    {
        private readonly BookingService _service;

        public BookingController(BookingService service)
        {
            _service = service;
        }

        [HttpGet]
        public IActionResult GetAll()
        {
            var entities = _service.GetBooking();

            if (entities == null)
            {
                return NotFound(new ResponseHandler<GetBookingDto>
                {
                    Code = StatusCodes.Status404NotFound,
                    Status = HttpStatusCode.NotFound.ToString(),
                    Message = "Data not Found!"
                });
            }
            return Ok(new ResponseHandler<IEnumerable<GetBookingDto>>
            {
                Code = StatusCodes.Status200OK,
                Status = HttpStatusCode.OK.ToString(),
                Message = "Data Found",
                Data = entities
            });
        }

        [HttpGet("{guid}")]
        public IActionResult GetByGuid(Guid guid)
        {
            var entities = _service.GetBookingGuid(guid);
            if (entities is null)
            {
                return NotFound(new ResponseHandler<GetBookingDto>
                {
                    Code = StatusCodes.Status404NotFound,
                    Status = HttpStatusCode.NotFound.ToString(),
                    Message = "Data not Found"
                });
            }
            return Ok(new ResponseHandler<GetBookingDto>
            {
                Code = StatusCodes.Status200OK,
                Status = HttpStatusCode.OK.ToString(),
                Message = "Data has Found",
                Data = entities
            });
        }

        [HttpPost]
        public IActionResult Create(NewBookingDto newBookingDto)
        {
            var entities = _service.CreateBooking(newBookingDto);
            if (entities is null)
            {
                return BadRequest(new ResponseHandler<GetBookingDto>
                {
                    Code = StatusCodes.Status400BadRequest,
                    Status = HttpStatusCode.BadRequest.ToString(),
                    Message = "Failed To Create",
                });
            }
            return Ok(new ResponseHandler<GetBookingDto>
            {
                Code = StatusCodes.Status201Created,
                Status = HttpStatusCode.OK.ToString(),
                Message = "Success to create",
                Data = entities
            });
        }

        [HttpPut]
        public IActionResult Update(UpdateBookingDto updateBooking)
        {
            var isFound = _service.UpdateBooking(updateBooking);
            if (isFound is -1)
            {
                return NotFound(new ResponseHandler<UpdateBookingDto>
                {
                    Code = StatusCodes.Status404NotFound,
                    Status = HttpStatusCode.InternalServerError.ToString(),
                    Message = "Id Not Found"
                });
            }
            if (isFound is 0)
            {
                return BadRequest(new ResponseHandler<UpdateBookingDto>
                {
                    Code = StatusCodes.Status500InternalServerError,
                    Status = HttpStatusCode.NotFound.ToString(),
                    Message = "Check your data"
                });
            }
            return Ok(new ResponseHandler<UpdateBookingDto>
            {
                Code = StatusCodes.Status200OK,
                Status = HttpStatusCode.OK.ToString(),
                Message = "Succesfull Updated"
            });
        }

        [HttpDelete]
        public IActionResult Delete(Guid guid)
        {

            var isFound = _service.DeleteBooking(guid);
            if (isFound is -1)
            {
                return NotFound(new ResponseHandler<GetBookingDto>
                {
                    Code = StatusCodes.Status404NotFound,
                    Status = HttpStatusCode.NotFound.ToString(),
                    Message = "Id not found"
                });
            }
            if (isFound is 0)
            {
                return BadRequest(new ResponseHandler<GetBookingDto>
                {
                    Code = StatusCodes.Status500InternalServerError,
                    Status = HttpStatusCode.InternalServerError.ToString(),
                    Message = "Check Your Data"
                });
            }
            return Ok(new ResponseHandler<GetBookingDto>
            {
                Code = StatusCodes.Status200OK,
                Status = HttpStatusCode.OK.ToString(),
                Message = "Succesfull Delete"
            });
        }

        [HttpPost("DetailBooking")]
        public IActionResult DetailBooking()
        {
            var books = _service.BookingDetail();
            if (books == null)
            {
                return NotFound(new ResponseHandler<DetailsBooking>
                {
                    Code = StatusCodes.Status404NotFound,
                    Status = HttpStatusCode.NotFound.ToString(),
                    Message = "Id not found"
                });
            }
            return Ok(new ResponseHandler<IEnumerable<DetailsBooking>>
            {
                Code = StatusCodes.Status200OK,
                Status = HttpStatusCode.OK.ToString(),
                Message = "Data Found !!",
                Data = books
            });
        }

        [HttpGet("DetailsByGuid")]
        public IActionResult DetailBookingByGuid(Guid guid)
        {
            var books = _service.BookingDetail(guid);
            if (books == null)
            {
                return NotFound(new ResponseHandler<DetailsBooking>
                {
                    Code = StatusCodes.Status404NotFound,
                    Status = HttpStatusCode.NotFound.ToString(),
                    Message = "Id not found"
                });
            }
            return Ok(new ResponseHandler<DetailsBooking>
            {
                Code = StatusCodes.Status200OK,
                Status = HttpStatusCode.OK.ToString(),
                Message = "Data Found !!",
                Data = books
            });

        }

        [HttpGet("GetBookingRoomDetails")]
        public IActionResult BookingRoomDetail()
        {
            var books = _service.BookingRoomDetail();
            if (books == null)
            {
                return NotFound(new ResponseHandler<BookingDetailsDto>
                {
                    Code = StatusCodes.Status404NotFound,
                    Status = HttpStatusCode.NotFound.ToString(),
                    Message = "Data not found"
                });
            }
            else
            {
                return Ok(new ResponseHandler<IEnumerable<BookingDetailsDto>>
                {
                    Code = StatusCodes.Status200OK,
                    Status = HttpStatusCode.OK.ToString(),
                    Message = "Data Found !!",
                    Data = books!
                });
            }
        }

        [HttpGet("BookingLength")]
        public IActionResult BookingLength()
        {
            var books = _service.BookingDuration();
            if (books == null)
            {
                return NotFound(new ResponseHandler<BookingLenghtDto>
                {
                    Code = StatusCodes.Status404NotFound,
                    Status = HttpStatusCode.NotFound.ToString(),
                    Message = "Data not found"
                });
            }
            else
            {
                return Ok(new ResponseHandler<IEnumerable<BookingLenghtDto>>
                {
                    Code = StatusCodes.Status200OK,
                    Status = HttpStatusCode.OK.ToString(),
                    Message = "Data Found !!",
                    Data = books
                });
            }
        }

    }
}
