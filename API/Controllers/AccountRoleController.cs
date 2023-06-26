using API.Contracts;
using API.Models;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [ApiController]
    [Route("api/AccountRoles")]
    public class AccountRoleController : GeneralController<IAccountRole, AccountRole>
    {

        public AccountRoleController(IAccountRole accountRole) : base(accountRole)
        {

        }
    }
}
