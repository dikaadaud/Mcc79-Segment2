using API.DTOs.AccountRole;
using API.Services;
using API.Ultilities.Handler;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace API.Controllers
{
    [ApiController]
    [Route("api/AccountRoles")]
    public class AccountRoleController : ControllerBase
    {
        private readonly AccountRoleService _service;

        public AccountRoleController(AccountRoleService service)
        {
            _service = service;
        }

        [HttpGet]
        public IActionResult GetAll()
        {
            var entities = _service.GetAccountRole();

            if (entities == null)
            {
                return NotFound(new ResponseHandler<GetAccountRoleDto>
                {
                    Code = StatusCodes.Status404NotFound,
                    Status = HttpStatusCode.NotFound.ToString(),
                    Message = "Data not Found!"
                });
            }
            return Ok(new ResponseHandler<IEnumerable<GetAccountRoleDto>>
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
            var entities = _service.GetAccRoleGuid(guid);
            if (entities is null)
            {
                return NotFound(new ResponseHandler<GetAccountRoleDto>
                {
                    Code = StatusCodes.Status404NotFound,
                    Status = HttpStatusCode.NotFound.ToString(),
                    Message = "Data not Found"
                });
            }
            return Ok(new ResponseHandler<GetAccountRoleDto>
            {
                Code = StatusCodes.Status200OK,
                Status = HttpStatusCode.OK.ToString(),
                Message = "Data has Found",
                Data = entities
            });
        }

        [HttpPost]
        public IActionResult Create(NewAccountRole newAccRoleDto)
        {
            var entities = _service.CreateAccRole(newAccRoleDto);
            if (entities is null)
            {
                return BadRequest(new ResponseHandler<GetAccountRoleDto>
                {
                    Code = StatusCodes.Status400BadRequest,
                    Status = HttpStatusCode.BadRequest.ToString(),
                    Message = "Failed To Create",
                });
            }
            return Ok(new ResponseHandler<GetAccountRoleDto>
            {
                Code = StatusCodes.Status201Created,
                Status = HttpStatusCode.OK.ToString(),
                Message = "Success to create",
                Data = entities
            });
        }

        [HttpPut]
        public IActionResult Update(UpdateAccountRole updateAccRole)
        {
            var isFound = _service.UpdateAccRole(updateAccRole);
            if (isFound is -1)
            {
                return NotFound(new ResponseHandler<UpdateAccountRole>
                {
                    Code = StatusCodes.Status404NotFound,
                    Status = HttpStatusCode.InternalServerError.ToString(),
                    Message = "Id Not Found"
                });
            }
            if (isFound is 0)
            {
                return BadRequest(new ResponseHandler<UpdateAccountRole>
                {
                    Code = StatusCodes.Status500InternalServerError,
                    Status = HttpStatusCode.NotFound.ToString(),
                    Message = "Check your data"
                });
            }
            return Ok(new ResponseHandler<UpdateAccountRole>
            {
                Code = StatusCodes.Status200OK,
                Status = HttpStatusCode.OK.ToString(),
                Message = "Succesfull Updated"
            });
        }

        [HttpDelete]
        public IActionResult Delete(Guid guid)
        {

            var isFound = _service.DeleteRoom(guid);
            if (isFound is -1)
            {
                return NotFound(new ResponseHandler<GetAccountRoleDto>
                {
                    Code = StatusCodes.Status404NotFound,
                    Status = HttpStatusCode.NotFound.ToString(),
                    Message = "Id not found"
                });
            }
            if (isFound is 0)
            {
                return BadRequest(new ResponseHandler<GetAccountRoleDto>
                {
                    Code = StatusCodes.Status500InternalServerError,
                    Status = HttpStatusCode.InternalServerError.ToString(),
                    Message = "Check Your Data"
                });
            }
            return Ok(new ResponseHandler<GetAccountRoleDto>
            {
                Code = StatusCodes.Status200OK,
                Status = HttpStatusCode.OK.ToString(),
                Message = "Succesfull Delete"
            });
        }
    }
}
