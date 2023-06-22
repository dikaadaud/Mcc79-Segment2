using API.Contracts;
using API.Models;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [ApiController]
    [Route("api/Employees")]
    public class EmployeeController : ControllerBase
    {
        private readonly IEmployeeRepository _employeeRepository;

        public EmployeeController(IEmployeeRepository employeeRepository)
        {
            _employeeRepository = employeeRepository;
        }

        [HttpGet]
        public IActionResult GetAll()
        {
            var employees = _employeeRepository.GetAll();
            if (employees == null)
            {
                return NotFound();
            }
            return Ok(employees);
        }

        [HttpGet("{guid}")]
        public IActionResult GetByGuid(Guid guid)
        {
            var employees = _employeeRepository.GetByGuid(guid);
            if (employees == null)
            {
                return NotFound();
            }
            return Ok(employees);
        }

        [HttpPost]
        public IActionResult Create(Employee employee)
        {
            var employees = _employeeRepository.Create(employee);
            if (employees == null)
            {
                return NotFound();
            }
            return Ok(employees);
        }

        [HttpPut]
        public IActionResult Update(Employee employee)
        {
            var employees = _employeeRepository.Update(employee);
            if (!employees)
            {
                return NotFound();
            }
            return Ok();
        }

        [HttpDelete]
        public IActionResult Delete(Guid guid)
        {
            var employees = _employeeRepository.Delete(guid);
            if (!employees)
            {
                return NotFound();
            }
            return Ok();
        }
    }
}
