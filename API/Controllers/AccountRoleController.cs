using API.Contracts;
using API.Models;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [ApiController]
    [Route("api/AccountRoles")]
    public class AccountRoleController : ControllerBase
    {
        private readonly IAccountRole _accountRole;

        public AccountRoleController(IAccountRole accountRole)
        {
            _accountRole = accountRole;
        }

        [HttpGet]
        public IActionResult GetAll()
        {
            var accRole = _accountRole.GetAll();
            if (!accRole.Any())
            {
                return NotFound();
            }
            return Ok(accRole);
        }

        [HttpGet("{guid}")]
        public IActionResult GetByGuid(Guid guid)
        {
            var accRole = _accountRole.GetByGuid(guid);
            if (accRole == null)
            {
                return NotFound();
            }
            return Ok(accRole);
        }

        [HttpPost]
        public IActionResult Create(AccountRole accountRole)
        {
            var accRole = _accountRole.Create(accountRole);
            if (accRole == null)
            {
                return NotFound();
            }
            return Ok(accRole);
        }

        [HttpPut]
        public IActionResult Update(AccountRole accountRole)
        {
            var accRole = _accountRole.Update(accountRole);
            if (!accRole)
            {
                return NotFound();
            }
            return Ok();
        }

        [HttpDelete]
        public IActionResult Delete(Guid guid)
        {
            var accRole = _accountRole.Delete(guid);
            if (!accRole)
            {
                return NotFound();
            }
            return Ok();
        }
    }
}
