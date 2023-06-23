using API.Contracts;
using API.Data;
using API.Models;

namespace API.Repositories
{
    public class RoleRepository : GeneralRepository<Role>, IRolesRepository
    {
        public RoleRepository(BookingDbContext context) : base(context)
        {
        }
    }
}
