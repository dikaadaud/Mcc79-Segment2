using API.Contracts;
using API.DTOs.Role;
using API.Models;

namespace API.Services
{
    public class RoleService
    {
        private readonly IRolesRepository _repository;

        public RoleService(IRolesRepository repository)
        {
            _repository = repository;
        }


        public IEnumerable<GetDtoRole>? GetRoles()
        {
            var roles = _repository.GetAll();
            if (!roles.Any())
            {
                return null;
            }

            // Ini Pake Linq buat mapping
            var toDto = roles.Select(r => new GetDtoRole
            {
                Guid = r.Guid,
                Name = r.Name,
            });

            return toDto;
        }

        public GetDtoRole? GetRoleGuid(Guid guid)
        {
            var roles = _repository.GetByGuid(guid);
            if (roles == null)
            {
                return null;
            }

            var toDto = new GetDtoRole
            {
                Guid = roles.Guid,
                Name = roles.Name
            };

            return toDto;

        }

        public GetDtoRole? CreateRole(NewRoleDto newRole)
        {
            var roles = new Role
            {
                Name = newRole.Name ?? "User",
                Guid = new Guid(),
                CreatedDate = DateTime.Now,
                ModifiedDate = DateTime.Now,
            };

            var createdRoles = _repository.Create(roles);
            if (createdRoles == null)
            {
                return null;
            }

            var toDto = new GetDtoRole
            {
                Guid = createdRoles.Guid,
                Name = createdRoles.Name,
            };

            return toDto;

        }

        public int UpdateRole(UpdateDtoRole updateRoleDto)
        {
            var isExist = _repository.isExist(updateRoleDto.Guid);
            if (!isExist)
            {
                return -1;
            }

            var getRole = _repository.GetByGuid(updateRoleDto.Guid);

            var roles = new Role
            {
                Name = updateRoleDto.Name,
                Guid = updateRoleDto.Guid,
                ModifiedDate = DateTime.Now,
                CreatedDate = getRole!.CreatedDate
            };

            var isUpdate = _repository.Update(roles);
            if (!isUpdate)
            {
                return 0;
            }
            return 1;
        }

        public int DeleteRole(Guid guid)
        {
            var isExist = _repository.isExist(guid);
            if (!isExist)
            {
                return -1;
            }
            var roles = _repository.GetByGuid(guid);
            var deleteRole = _repository.Delete(roles!);
            if (!deleteRole)
            {
                return 0;
            }
            return 1;
        }
    }
}
