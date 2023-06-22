using API.Contracts;
using API.Data;
using API.Models;

namespace API.Repositories
{
    public class RoleRepository : IRolesRepository
    {
        private readonly BookingDbContext _context;

        public RoleRepository(BookingDbContext context)
        {
            _context = context;
        }

        public Role Create(Role role)
        {
            try
            {
                _context.Roles.Add(role);
                _context.SaveChanges();
                return role;
            }
            catch (Exception)
            {
                return new Role();
            }
;
        }

        public bool Delete(Guid guid)
        {
            var roles = GetById(guid);

            if (roles == null)
            {
                return false;
            }

            _context.Roles.Remove(roles);
            _context.SaveChanges();
            return true;
        }

        public ICollection<Role> GetAll()
        {
            return _context.Set<Role>().ToList();
        }

        public Role? GetById(Guid guid)
        {
            return _context.Set<Role>().Find(guid);
        }

        public bool Update(Role role)
        {
            try
            {
                _context.Set<Role>().Update(role);
                _context.SaveChanges();
                return true;
            }
            catch (Exception)
            {

                return false;
            }
        }
    }
}
