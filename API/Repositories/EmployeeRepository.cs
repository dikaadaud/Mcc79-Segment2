using API.Contracts;
using API.Data;
using API.Models;

namespace API.Repositories
{
    public class EmployeeRepository : IEmployeeRepository
    {
        private readonly BookingDbContext _context;

        public EmployeeRepository(BookingDbContext context)
        {
            _context = context;
        }

        public Employee Create(Employee employee)
        {
            try
            {
                _context.Set<Employee>().Add(employee);
                _context.SaveChanges();
                return employee;
            }
            catch (Exception)
            {
                return new Employee();
            }
        }

        public bool Delete(Guid guid)
        {
            var emp = GetByGuid(guid);
            if (emp == null)
            {
                return false;
            }
            try
            {
                _context.Set<Employee>().Remove(emp);
                _context.SaveChanges();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public ICollection<Employee> GetAll()
        {
            return _context.Set<Employee>().ToList();
        }

        public Employee? GetByGuid(Guid guid)
        {
            return _context.Set<Employee>().Find();
        }

        public bool Update(Employee employee)
        {
            try
            {
                _context.Set<Employee>().Update(employee);
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
