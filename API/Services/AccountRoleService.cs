using API.Contracts;
using API.DTOs.AccountRole;
using API.Models;

namespace API.Services
{
    public class AccountRoleService
    {
        private readonly IAccountRole _repository;

        public AccountRoleService(IAccountRole repository)
        {
            _repository = repository;
        }


        public IEnumerable<GetAccountRoleDto>? GetAccountRole()
        {
            var accRole = _repository.GetAll();
            if (!accRole.Any())
            {
                return null;
            }

            // Ini Pake Linq buat mapping
            var toDto = accRole.Select(acc => new GetAccountRoleDto
            {
                Guid = acc.Guid,
                AccountGuid = acc.AccountGuid,
                RoleGuid = acc.RoleGuid,
            });

            return toDto;
        }

        public GetAccountRoleDto? GetAccRoleGuid(Guid guid)
        {
            var accRole = _repository.GetByGuid(guid);
            if (accRole == null)
            {
                return null;
            }

            var toDto = new GetAccountRoleDto
            {
                Guid = accRole.Guid,
                AccountGuid = accRole.AccountGuid,
                RoleGuid = accRole.RoleGuid,
            };

            return toDto;

        }

        public GetAccountRoleDto? CreateAccRole(NewAccountRole newAccRole)
        {
            var accRole = new AccountRole
            {
                Guid = new Guid(),
                CreatedDate = DateTime.Now,
                ModifiedDate = DateTime.Now,
                RoleGuid = newAccRole.RoleGuid,
                AccountGuid = newAccRole.AccountGuid

            };

            var createdAccRole = _repository.Create(accRole);
            if (createdAccRole == null)
            {
                return null;
            }

            var toDto = new GetAccountRoleDto
            {
                Guid = createdAccRole.Guid,
                AccountGuid = createdAccRole.AccountGuid,
                RoleGuid = createdAccRole.RoleGuid
            };

            return toDto;

        }

        public int UpdateAccRole(UpdateAccountRole updateAccDto)
        {
            var isExist = _repository.isExist(updateAccDto.Guid);
            if (!isExist)
            {
                return -1;
            }

            var getAccRole = _repository.GetByGuid(updateAccDto.Guid);

            var accRole = new AccountRole
            {
                Guid = updateAccDto.Guid,
                AccountGuid = updateAccDto.AccountGuid,
                RoleGuid = updateAccDto.RoleGuid,
                ModifiedDate = DateTime.Now,
                CreatedDate = getAccRole.CreatedDate

            };

            var isUpdate = _repository.Update(accRole);
            if (!isUpdate)
            {
                return 0;
            }
            return 1;
        }

        public int DeleteRoom(Guid guid)
        {
            var isExist = _repository.isExist(guid);
            if (!isExist)
            {
                return -1;
            }
            var accRole = _repository.GetByGuid(guid);
            var deleteAccRole = _repository.Delete(accRole!);
            if (!deleteAccRole)
            {
                return 0;
            }
            return 1;
        }
    }
}
