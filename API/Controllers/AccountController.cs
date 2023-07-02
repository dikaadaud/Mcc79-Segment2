using API.DTOs.Account;
using API.Services;
using API.Ultilities.Handler;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace API.Controllers
{
    [ApiController]
    [Route("api/Accounts")]
    public class AccountController : ControllerBase
    {
        private readonly AccountService _service;
        public AccountController(AccountService service)
        {
            _service = service;

        }

        [HttpGet]
        public IActionResult GetAll()
        {
            var entities = _service.GetAccount();

            if (!entities.Any())
            {
                return NotFound(new ResponseHandler<GetAccountDto>
                {
                    Code = StatusCodes.Status404NotFound,
                    Status = HttpStatusCode.NotFound.ToString(),
                    Message = "Data not Found!"
                });
            }
            return Ok(new ResponseHandler<IEnumerable<GetAccountDto>>
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
            var entities = _service.GetAccGuid(guid);
            if (entities is null)
            {
                return NotFound(new ResponseHandler<GetAccountDto>
                {
                    Code = StatusCodes.Status404NotFound,
                    Status = HttpStatusCode.NotFound.ToString(),
                    Message = "Data not Found"
                });
            }
            return Ok(new ResponseHandler<GetAccountDto>
            {
                Code = StatusCodes.Status200OK,
                Status = HttpStatusCode.OK.ToString(),
                Message = "Data has Found",
                Data = entities
            });
        }

        [HttpPost]
        public IActionResult Create(NewAccountDto newAccDto)
        {
            var entities = _service.CreateAccount(newAccDto);
            if (entities is null)
            {
                return BadRequest(new ResponseHandler<GetAccountDto>
                {
                    Code = StatusCodes.Status400BadRequest,
                    Status = HttpStatusCode.BadRequest.ToString(),
                    Message = "Failed To Create",
                });
            }
            return Ok(new ResponseHandler<GetAccountDto>
            {
                Code = StatusCodes.Status201Created,
                Status = HttpStatusCode.OK.ToString(),
                Message = "Success to create",
                Data = entities
            });
        }

        [HttpPut]
        public IActionResult Update(UpdateAccountDto updateAcc)
        {
            var isFound = _service.UpdateAccount(updateAcc);
            if (isFound is -1)
            {
                return NotFound(new ResponseHandler<UpdateAccountDto>
                {
                    Code = StatusCodes.Status404NotFound,
                    Status = HttpStatusCode.InternalServerError.ToString(),
                    Message = "Id Not Found"
                });
            }
            if (isFound is 0)
            {
                return BadRequest(new ResponseHandler<UpdateAccountDto>
                {
                    Code = StatusCodes.Status500InternalServerError,
                    Status = HttpStatusCode.NotFound.ToString(),
                    Message = "Check your data"
                });
            }
            return Ok(new ResponseHandler<UpdateAccountDto>
            {
                Code = StatusCodes.Status200OK,
                Status = HttpStatusCode.OK.ToString(),
                Message = "Succesfull Updated"
            });
        }

        [HttpDelete]
        public IActionResult Delete(Guid guid)
        {

            var isFound = _service.DeleteAcc(guid);
            if (isFound is -1)
            {
                return NotFound(new ResponseHandler<GetAccountDto>
                {
                    Code = StatusCodes.Status404NotFound,
                    Status = HttpStatusCode.NotFound.ToString(),
                    Message = "Id not found"
                });
            }
            if (isFound is 0)
            {
                return BadRequest(new ResponseHandler<GetAccountDto>
                {
                    Code = StatusCodes.Status500InternalServerError,
                    Status = HttpStatusCode.InternalServerError.ToString(),
                    Message = "Check Your Data"
                });
            }
            return Ok(new ResponseHandler<GetAccountDto>
            {
                Code = StatusCodes.Status200OK,
                Status = HttpStatusCode.OK.ToString(),
                Message = "Succesfull Delete"
            });
        }

        [HttpPost("register")]
        public IActionResult Register(RegisterDto registerDto)
        {
            var entities = _service.RegisterAccount(registerDto);
            if (entities is null)
            {
                return BadRequest(new ResponseHandler<RegisterDto>
                {
                    Code = StatusCodes.Status400BadRequest,
                    Status = HttpStatusCode.BadRequest.ToString(),
                    Message = "Failed To Register",
                });
            }
            return Ok(new ResponseHandler<RegisterDto>
            {
                Code = StatusCodes.Status201Created,
                Status = HttpStatusCode.OK.ToString(),
                Message = "Success to create",
                Data = entities
            });
        }

        [HttpPost("Login")]
        public IActionResult LoginRequest(Login login)
        {
            var entities = _service.Login(login);

            if (entities == "-1")
            {
                return NotFound(new ResponseHandler<Login>
                {
                    Code = StatusCodes.Status404NotFound,
                    Status = HttpStatusCode.BadRequest.ToString(),
                    Message = "Data Not Found"
                });
            }
            if (entities == "0")
            {
                return BadRequest(new ResponseHandler<Login>
                {
                    Code = StatusCodes.Status400BadRequest,
                    Status = HttpStatusCode.BadRequest.ToString(),
                    Message = "Bad Request"
                });
            }
            if (entities == "-2")
            {
                return BadRequest(new ResponseHandler<Login>
                {
                    Code = StatusCodes.Status500InternalServerError,
                    Status = HttpStatusCode.InternalServerError.ToString(),
                    Message = "Data Error"
                });
            }

            return Ok(new ResponseHandler<string>
            {
                Code = StatusCodes.Status200OK,
                Status = HttpStatusCode.OK.ToString(),
                Message = "Success to Login",
                Data = entities
            });


        }

        [HttpPut("ChangePassword")]
        public IActionResult ChangePassword(ChangePasswordDto changePassword)
        {
            var update = _service.ChangePassword(changePassword);
            if (update == 0)
            {
                return NotFound(new ResponseHandler<ChangePasswordDto>
                {
                    Code = StatusCodes.Status404NotFound,
                    Status = HttpStatusCode.BadRequest.ToString(),
                    Message = "Email Not Found"
                });
            }
            if (update == 1)
            {
                return NotFound(new ResponseHandler<ChangePasswordDto>
                {
                    Code = StatusCodes.Status404NotFound,
                    Status = HttpStatusCode.BadRequest.ToString(),
                    Message = "Otp Has been Used"
                });
            }
            if (update == 2)
            {
                return NotFound(new ResponseHandler<ChangePasswordDto>
                {
                    Code = StatusCodes.Status404NotFound,
                    Status = HttpStatusCode.BadRequest.ToString(),
                    Message = "Email Not Found"
                });
            }

            return Ok(new ResponseHandler<ChangePasswordDto>
            {
                Code = StatusCodes.Status200OK,
                Status = HttpStatusCode.OK.ToString(),
                Message = "Success Updated",
            });


        }

    }
}


