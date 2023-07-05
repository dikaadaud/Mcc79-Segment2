using API.Models;

namespace API.Contracts
{
    public interface IRolesRepository : IGeneralRepository<Role>
    {
        Role? GetByName(string name);
    }
}
