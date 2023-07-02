using API.Models;

namespace API.Contracts
{
    public interface IEmployeeRepository : IGeneralRepository<Employee>
    {
        Employee? getEmailandPhone(string email);
        Employee? GetEmail(string email);
    }
}
