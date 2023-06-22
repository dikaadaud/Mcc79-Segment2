using API.Models;

namespace API.Contracts
{
    public interface IRolesRepository
    {
        ICollection<Role> GetAll();
        Role? GetById(Guid guid);
        Role Create(Role role);
        bool Update(Role role);
        bool Delete(Guid guid);
    }
}
