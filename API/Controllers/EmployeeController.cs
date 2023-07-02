using API.DTOs.Employee;
using API.Services;
using API.Ultilities.Handler;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace API.Controllers
{
    [ApiController]
    [Route("api/Employees")]
    public class EmployeeController : ControllerBase
    {
        private readonly EmployeeeService _service;

        public EmployeeController(EmployeeeService service)
        {
            _service = service;
        }

        [HttpGet]
        public IActionResult GetAll()
        {
            var entities = _service.GetEmployee();

            if (entities is null)
            {
                return NotFound(new ResponseHandler<GetEmployeeDto>
                {
                    Code = StatusCodes.Status404NotFound,
                    Status = HttpStatusCode.NotFound.ToString(),
                    Message = "Data not Found!"
                });
            }
            return Ok(new ResponseHandler<IEnumerable<GetEmployeeDto>>
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
            var entities = _service.GetEmployeeGuid(guid);
            if (entities is null)
            {
                return NotFound(new ResponseHandler<GetEmployeeDto>
                {
                    Code = StatusCodes.Status404NotFound,
                    Status = HttpStatusCode.NotFound.ToString(),
                    Message = "Data not Found"
                });
            }
            return Ok(new ResponseHandler<GetEmployeeDto>
            {
                Code = StatusCodes.Status200OK,
                Status = HttpStatusCode.OK.ToString(),
                Message = "Data has Found",
                Data = entities
            });
        }

        [HttpPost]
        public IActionResult Create(NewEmployee newEmployeeDto)
        {
            var entities = _service.CreateEmployee(newEmployeeDto);
            if (entities is null)
            {
                return BadRequest(new ResponseHandler<GetEmployeeDto>
                {
                    Code = StatusCodes.Status400BadRequest,
                    Status = HttpStatusCode.BadRequest.ToString(),
                    Message = "Failed To Create",
                });
            }
            return Ok(new ResponseHandler<GetEmployeeDto>
            {
                Code = StatusCodes.Status201Created,
                Status = HttpStatusCode.OK.ToString(),
                Message = "Success to create",
                Data = entities
            });
        }

        [HttpPut]
        public IActionResult Update(UpdateEmployeeDto updateEmployee)
        {
            var isFound = _service.UpdateEmployee(updateEmployee);
            if (isFound is -1)
            {
                return NotFound(new ResponseHandler<UpdateEmployeeDto>
                {
                    Code = StatusCodes.Status404NotFound,
                    Status = HttpStatusCode.InternalServerError.ToString(),
                    Message = "Id Not Found"
                });
            }
            if (isFound is 0)
            {
                return BadRequest(new ResponseHandler<UpdateEmployeeDto>
                {
                    Code = StatusCodes.Status500InternalServerError,
                    Status = HttpStatusCode.NotFound.ToString(),
                    Message = "Check your data"
                });
            }
            return Ok(new ResponseHandler<UpdateEmployeeDto>
            {
                Code = StatusCodes.Status200OK,
                Status = HttpStatusCode.OK.ToString(),
                Message = "Succesfull Updated"
            });
        }

        [HttpDelete]
        public IActionResult Delete(Guid guid)
        {

            var isFound = _service.DeleteEmployee(guid);
            if (isFound is -1)
            {
                return NotFound(new ResponseHandler<GetEmployeeDto>
                {
                    Code = StatusCodes.Status404NotFound,
                    Status = HttpStatusCode.NotFound.ToString(),
                    Message = "Id not found"
                });
            }
            if (isFound is 0)
            {
                return BadRequest(new ResponseHandler<GetEmployeeDto>
                {
                    Code = StatusCodes.Status500InternalServerError,
                    Status = HttpStatusCode.InternalServerError.ToString(),
                    Message = "Check Your Data"
                });
            }
            return Ok(new ResponseHandler<GetEmployeeDto>
            {
                Code = StatusCodes.Status200OK,
                Status = HttpStatusCode.OK.ToString(),
                Message = "Succesfull Delete"
            });
        }

        [HttpPost("GetEmployeeEdu")]
        public IActionResult GetMaster()
        {
            var master = _service.GetMaster();
            if (master == null)
            {
                return NotFound(new ResponseHandler<EmployeEducationDto>
                {
                    Code = StatusCodes.Status404NotFound,
                    Status = HttpStatusCode.NotFound.ToString(),
                    Message = "Data not found"
                });
            }
            else
            {
                return Ok(new ResponseHandler<IEnumerable<EmployeEducationDto>>
                {
                    Code = StatusCodes.Status200OK,
                    Status = HttpStatusCode.OK.ToString(),
                    Message = "Data Found !!",
                    Data = master

                });
            }
        }

        public IActionResult GetMasterByGuid(Guid guid)
        {
            var master = _service.GetMasterByGuid(guid);
            if (master == null)
            {
                return NotFound(new ResponseHandler<EmployeEducationDto>
                {
                    Code = StatusCodes.Status404NotFound,
                    Status = HttpStatusCode.NotFound.ToString(),
                    Message = "ID not found"
                });
            }
            else
            {
                return Ok(new ResponseHandler<EmployeEducationDto>
                {
                    Code = StatusCodes.Status200OK,
                    Status = HttpStatusCode.OK.ToString(),
                    Message = "Data Found !!",
                    Data = master
                });
            }
        }


    }

}
