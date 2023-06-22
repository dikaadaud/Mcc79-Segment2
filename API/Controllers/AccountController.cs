using API.Contracts;
using API.Models;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [ApiController]
    [Route("api/Accounts")]
    public class AccountController : ControllerBase
    {
        private readonly IAccountRepository _accountRepository;

        public AccountController(IAccountRepository accountRepository)
        {
            _accountRepository = accountRepository;
        }

        [HttpGet]
        public IActionResult GetAll()
        {
            var accounts = _accountRepository.GetAll();
            if (accounts == null)
            {
                return NotFound();
            }
            return Ok(accounts);
        }

        [HttpGet("{guid}")]
        public IActionResult GetByGuid(Guid guid)
        {
            var account = _accountRepository.GetByGuid(guid);
            if (account == null)
            {
                return NotFound();
            }
            return Ok(account);
        }

        [HttpPost]
        public IActionResult Create(Account accounts)
        {
            var account = _accountRepository.Create(accounts);
            if (account == null)
            {
                return NotFound();
            }
            return Ok(account);
        }

        [HttpPut]
        public IActionResult Update(Account accounts)
        {
            var account = _accountRepository.Update(accounts);
            if (!account)
            {
                return NotFound();
            }
            return Ok();
        }

        [HttpDelete]
        public IActionResult Delete(Guid guid)
        {
            var booking = _accountRepository.Delete(guid);
            if (!booking)
            {
                return NotFound();
            }
            return Ok();
        }
    }
}
