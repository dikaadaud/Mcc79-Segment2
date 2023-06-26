using API.Contracts;
using API.DTOs.Universities;
using API.Models;

namespace API.Services
{
    public class UniversityService
    {
        private readonly IUniversityRepository _repository;

        public UniversityService(IUniversityRepository repository)
        {
            _repository = repository;
        }


        public IEnumerable<GetUniversityDTO>? GetUniversity()
        {
            var univ = _repository.GetAll();
            if (!univ.Any())
            {
                return null;
            }

            //var toDto = new List<GetUniversityDTO>();

            //foreach (var univer in univ)
            //{
            //    toDto.Add(new GetUniversityDTO
            //    {
            //        Code = univer.Code,
            //        Name = univer.Name,
            //        Guid = univer.Guid
            //    });
            //}

            // Ini Pake Linq buat mapping
            var toDto = univ.Select(u => new GetUniversityDTO
            {
                Guid = u.Guid,
                Code = u.Code,
                Name = u.Name
            });

            return toDto;
        }


        public IEnumerable<GetUniversityDTO>? GetByname(string name)
        {
            var univ = _repository.GetByName(name);
            if (!univ.Any())
            {
                return null;
            }

            var toDto = univ.Select(u => new GetUniversityDTO
            {
                Guid = u.Guid,
                Name = u.Name,
                Code = u.Code
            });

            return toDto;


        }

        public GetUniversityDTO? GetUniversity(Guid guid)
        {
            var univ = _repository.GetByGuid(guid);
            if (univ == null)
            {
                return null;
            }

            var toDto = new GetUniversityDTO
            {
                Guid = univ.Guid,
                Code = univ.Code,
                Name = univ.Name
            };

            return toDto;

        }

        public GetUniversityDTO? CreateUniversity(NewUniversityDto newUniv)
        {
            var univ = new University
            {
                Code = newUniv.Code,
                Name = newUniv.Name,
                Guid = new Guid(),
                CreatedDate = DateTime.Now,
                ModifiedDate = DateTime.Now
            };

            var createdUniv = _repository.Create(univ);
            if (createdUniv == null)
            {
                return null;
            }

            var toDto = new GetUniversityDTO
            {
                Guid = createdUniv.Guid,
                Code = createdUniv.Code,
                Name = createdUniv.Name
            };

            return toDto;

        }

        public int UpdateUniversity(UpdateUniversityDto updateUniversityDto)
        {
            var isExist = _repository.isExist(updateUniversityDto.Guid);
            if (!isExist)
            {
                return -1;
            }

            var getUniversity = _repository.GetByGuid(updateUniversityDto.Guid);

            var univ = new University
            {
                Code = updateUniversityDto.Code,
                Name = updateUniversityDto.Name,
                Guid = updateUniversityDto.Guid,
                ModifiedDate = DateTime.Now,
                CreatedDate = getUniversity.CreatedDate
            };

            var isUpdate = _repository.Update(univ);
            if (!isUpdate)
            {
                return 0;
            }
            return 1;
        }

        public int DeleteUniv(Guid guid)
        {
            var isExist = _repository.isExist(guid);
            if (!isExist)
            {
                return -1;
            }
            var university = _repository.GetByGuid(guid);
            var deleteuniv = _repository.Delete(university!);
            if (!deleteuniv)
            {
                return 0;
            }
            return 1;
        }


    }
}
