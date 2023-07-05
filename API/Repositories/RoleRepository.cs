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

        public Role? GetByName(string name)
        {
            return _context.Set<Role>().FirstOrDefault(x => x.Name == name);
        }
    }
}
