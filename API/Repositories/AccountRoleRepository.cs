using API.Contracts;
using API.Data;
using API.Models;

namespace API.Repositories
{
    public class AccountRoleRepository : IAccountRole
    {
        private readonly BookingDbContext _context;

        public AccountRoleRepository(BookingDbContext context)
        {
            _context = context;
        }

        public AccountRole Create(AccountRole AccountRoles)
        {

            try
            {

                _context.Set<AccountRole>().Add(AccountRoles);
                _context.SaveChanges();
                return AccountRoles;
            }
            catch (Exception)
            {
                return new AccountRole();
            }


        }

        public bool Delete(Guid guid)
        {
            var role = GetByGuid(guid);
            if (role is null)
            {
                return false;
            }
            try
            {
                _context.Set<AccountRole>().Remove(role);
                _context.SaveChanges();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public ICollection<AccountRole> GetAll()
        {
            return _context.Set<AccountRole>().ToList();
        }

        public AccountRole? GetByGuid(Guid guid)
        {
            return _context.Set<AccountRole>().Find(guid);
        }

        public bool Update(AccountRole AccountRoles)
        {
            try
            {
                _context.Set<AccountRole>().Update(AccountRoles);
                _context.SaveChanges();
                return true;
            }
            catch (Exception)
            {
                return false;
                throw;
            }
        }
    }
}
