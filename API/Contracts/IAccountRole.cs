using API.Models;

namespace API.Contracts
{
    public interface IAccountRole
    {
        ICollection<AccountRole> GetAll();
        AccountRole? GetByGuid(Guid guid);
        AccountRole Create(AccountRole AccountRoles);
        bool Update(AccountRole AccountRoles);
        bool Delete(Guid guid);
    }
}
