using API.Contracts;
using API.Data;
using API.Models;

namespace API.Repositories
{
    public class EmployeeRepository : GeneralRepository<Employee>, IEmployeeRepository
    {
        public EmployeeRepository(BookingDbContext context) : base(context)
        {
        }

        public Employee? GetEmail(string email)
        {
            return _context.Set<Employee>().SingleOrDefault(e => e.Email == email);
        }
    }
}
