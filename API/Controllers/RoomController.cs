using API.Contracts;
using API.Models;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [ApiController]
    [Route("api/Rooms")]
    public class RoomController : ControllerBase
    {
        private readonly IRoom _iroom;

        public RoomController(IRoom iroom)
        {
            _iroom = iroom;
        }

        [HttpGet]
        public IActionResult GetAll()
        {
            var rooms = _iroom.GetAll();
            if (!rooms.Any())
            {
                return NotFound();
            }

            return Ok(rooms);
        }

        [HttpGet("{guid}")]
        public IActionResult GetByGuid(Guid guid)
        {
            var rooms = _iroom.GetByGuid(guid);
            if (rooms is null)
            {
                return NotFound();
            }
            return Ok(rooms);
        }

        [HttpPost]
        public IActionResult Create(Room room)
        {
            var rooms = _iroom.Create(room);
            return Ok(rooms);
        }

        [HttpPut]
        public IActionResult Update(Room room)
        {
            var rooms = _iroom.Update(room);
            if (!rooms)
            {
                return NotFound();
            }

            return Ok();
        }

        [HttpDelete]
        public IActionResult Delete(Guid guid)
        {
            var rooms = _iroom.Delete(guid);
            if (!rooms)
            {
                return NotFound();
            }
            return Ok();
        }
    }
}
