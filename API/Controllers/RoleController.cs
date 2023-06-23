using API.Contracts;
using API.Models;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [ApiController]
    [Route("api/Roles")]

    public class RoleController : ControllerBase
    {
        private readonly IRolesRepository _rolesRepository;

        public RoleController(IRolesRepository rolesRepository)
        {
            _rolesRepository = rolesRepository;
        }

        [HttpGet]
        public IActionResult GetAll()
        {
            var role = _rolesRepository.GetAll();

            if (!role.Any())
            {
                return NotFound();
            }
            return Ok(role);
        }

        [HttpGet("{guid}")]
        public IActionResult GetByGuid(Guid guid)
        {
            var role = _rolesRepository.GetByGuid(guid);
            if (role is null)
            {
                return NotFound();
            }
            return Ok(role);
        }

        [HttpPost]
        public IActionResult Create(Role roles)
        {
            var role = _rolesRepository.Create(roles);
            if (role is null)
            {
                return NotFound();
            }
            return Ok(role);
        }

        [HttpPut]
        public IActionResult Update(Role roles)
        {
            var role = _rolesRepository.Update(roles);
            if (!role)
            {
                return NotFound();
            }
            return Ok();
        }

        [HttpDelete]
        public IActionResult Delete(Guid guid)
        {
            var role = _rolesRepository.Delete(guid);
            if (!role)
            {
                return NotFound();
            }
            return Ok();
        }
    }
}
