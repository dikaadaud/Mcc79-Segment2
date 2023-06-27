using API.DTOs.Role;
using API.Services;
using API.Ultilities.Handler;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace API.Controllers
{
    [ApiController]
    [Route("api/Roles")]

    public class RoleController : ControllerBase
    {
        private readonly RoleService _service;

        public RoleController(RoleService service)
        {
            _service = service;
        }

        [HttpGet]
        public IActionResult GetAll()
        {
            var entities = _service.GetRoles();

            if (!entities.Any())
            {
                return NotFound(new ResponseHandler<GetDtoRole>
                {
                    Code = StatusCodes.Status404NotFound,
                    Status = HttpStatusCode.NotFound.ToString(),
                    Message = "Data not Found!"
                });
            }
            return Ok(new ResponseHandler<IEnumerable<GetDtoRole>>
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
            var entities = _service.GetRoleGuid(guid);
            if (entities is null)
            {
                return NotFound(new ResponseHandler<GetDtoRole>
                {
                    Code = StatusCodes.Status404NotFound,
                    Status = HttpStatusCode.NotFound.ToString(),
                    Message = "Data not Found"
                });
            }
            return Ok(new ResponseHandler<GetDtoRole>
            {
                Code = StatusCodes.Status200OK,
                Status = HttpStatusCode.OK.ToString(),
                Message = "Data has Found",
                Data = entities
            });
        }

        [HttpPost]
        public IActionResult Create(NewRoleDto newDto)
        {
            var entities = _service.CreateRole(newDto);
            if (entities is null)
            {
                return BadRequest(new ResponseHandler<GetDtoRole>
                {
                    Code = StatusCodes.Status400BadRequest,
                    Status = HttpStatusCode.BadRequest.ToString(),
                    Message = "Failed To Create",
                });
            }
            return Ok(new ResponseHandler<GetDtoRole>
            {
                Code = StatusCodes.Status201Created,
                Status = HttpStatusCode.OK.ToString(),
                Message = "Success to create",
                Data = entities
            });
        }

        [HttpPut]
        public IActionResult Update(UpdateDtoRole updateRole)
        {
            var isFound = _service.UpdateRole(updateRole);
            if (isFound is -1)
            {
                return NotFound(new ResponseHandler<UpdateDtoRole>
                {
                    Code = StatusCodes.Status404NotFound,
                    Status = HttpStatusCode.InternalServerError.ToString(),
                    Message = "Id Not Found"
                });
            }
            if (isFound is 0)
            {
                return BadRequest(new ResponseHandler<UpdateDtoRole>
                {
                    Code = StatusCodes.Status500InternalServerError,
                    Status = HttpStatusCode.NotFound.ToString(),
                    Message = "Check your data"
                });
            }
            return Ok(new ResponseHandler<UpdateDtoRole>
            {
                Code = StatusCodes.Status200OK,
                Status = HttpStatusCode.OK.ToString(),
                Message = "Succesfull Updated"
            });
        }

        [HttpDelete]
        public IActionResult Delete(Guid guid)
        {

            var isFound = _service.DeleteRole(guid);
            if (isFound is -1)
            {
                return NotFound(new ResponseHandler<GetDtoRole>
                {
                    Code = StatusCodes.Status404NotFound,
                    Status = HttpStatusCode.NotFound.ToString(),
                    Message = "Id not found"
                });
            }
            if (isFound is 0)
            {
                return BadRequest(new ResponseHandler<GetDtoRole>
                {
                    Code = StatusCodes.Status500InternalServerError,
                    Status = HttpStatusCode.InternalServerError.ToString(),
                    Message = "Check Your Data"
                });
            }
            return Ok(new ResponseHandler<GetDtoRole>
            {
                Code = StatusCodes.Status200OK,
                Status = HttpStatusCode.OK.ToString(),
                Message = "Succesfull Delete"
            });
        }
    }
}
