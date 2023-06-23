using API.Contracts;
using API.Data;
using API.Models;

namespace API.Repositories
{
    public class AccountRoleRepository : GeneralRepository<AccountRole>, IAccountRole
    {
        public AccountRoleRepository(BookingDbContext context) : base(context)
        {
        }
    }
}
