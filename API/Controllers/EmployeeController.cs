using API.Contracts;
using API.Models;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [ApiController]
    [Route("api/Employees")]
    public class EmployeeController : GeneralController<IEmployeeRepository, Employee>
    {
        public EmployeeController(IEmployeeRepository employeeRepository) : base(employeeRepository)
        {
        }
    }
}
