using API.Contracts;
using API.Models;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [ApiController]
    [Route("api/Accounts")]
    public class AccountController : GeneralController<Account>
    {
        //private readonly IAccountRepository _accountRepository;

        public AccountController(IAccountRepository accountRepository) : base(accountRepository)
        {
        }
    }
}
