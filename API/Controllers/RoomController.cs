using API.Contracts;
using API.Models;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [ApiController]
    [Route("api/Rooms")]
    public class RoomController : GeneralController<Room>
    {


        public RoomController(IRoom iroom) : base(iroom)
        {

        }

    }
}
