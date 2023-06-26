using API.Contracts;
using API.Models;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [ApiController]
    [Route("api/Rooms")]
    public class RoomController : GeneralController<IRoom, Room>
    {
        public RoomController(IRoom iroom) : base(iroom)
        {

        }

    }
}
