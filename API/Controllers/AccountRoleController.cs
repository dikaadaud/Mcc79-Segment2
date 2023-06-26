using API.Contracts;
using API.Models;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [ApiController]
    [Route("api/AccountRoles")]
    public class AccountRoleController : GeneralController<AccountRole>
    {

        public AccountRoleController(IAccountRole accountRole) : base(accountRole)
        {

        }
    }
}
