using API.Contracts;
using API.DTOs.Education;
using API.Models;

namespace API.Services
{
    public class EducationService
    {
        private readonly IEducationRepository _repository;

        public EducationService(IEducationRepository repository)
        {
            _repository = repository;
        }


        public IEnumerable<GetEducationDto>? GetEducation()
        {
            var edu = _repository.GetAll();
            if (!edu.Any())
            {
                return null;
            }

            // Ini Pake Linq buat mapping
            var toDto = edu.Select(e => new GetEducationDto
            {
                Guid = e.Guid,
                UniversityGuid = e.UniversityGuid,
                Degree = e.Degree,
                Gpa = e.Gpa,
                Major = e.Major
            });

            return toDto;
        }

        public GetEducationDto? GetEduGuid(Guid guid)
        {
            var edu = _repository.GetByGuid(guid);
            if (edu == null)
            {
                return null;
            }

            var toDto = new GetEducationDto
            {
                Guid = edu.Guid,
                UniversityGuid = edu.UniversityGuid,
                Degree = edu.Degree,
                Gpa = edu.Gpa,
                Major = edu.Major
            };

            return toDto;

        }

        public GetEducationDto? CreateEducation(NewEducationDto newEducation)
        {
            var emp = new Education
            {
                Guid = newEducation.Guid,
                CreatedDate = DateTime.Now,
                ModifiedDate = DateTime.Now,
                Degree = newEducation.Degree,
                Gpa = newEducation.Gpa,
                Major = newEducation.Major,
                UniversityGuid = newEducation.UniversityGuid

            };

            var createdEducation = _repository.Create(emp);
            if (createdEducation == null)
            {
                return null;
            }

            var toDto = new GetEducationDto
            {
                Guid = createdEducation.Guid,
                Degree = createdEducation.Degree,
                Gpa = createdEducation.Gpa,
                Major = createdEducation.Major,
                UniversityGuid = createdEducation.UniversityGuid
            };

            return toDto;

        }

        public int UpdateEducation(UpdateEducationDto updateEduDto)
        {
            var isExist = _repository.isExist(updateEduDto.Guid);
            if (!isExist)
            {
                return -1;
            }

            var getEdu = _repository.GetByGuid(updateEduDto.Guid);

            var edu = new Education
            {
                Guid = updateEduDto.Guid,
                Degree = updateEduDto.Degree,
                Major = updateEduDto.Major,
                Gpa = updateEduDto.Gpa,
                CreatedDate = getEdu.CreatedDate,
                ModifiedDate = DateTime.Now,
                UniversityGuid = updateEduDto.UniversityGuid

            };

            var isUpdate = _repository.Update(edu);
            if (!isUpdate)
            {
                return 0;
            }
            return 1;
        }

        public int DeleteEdu(Guid guid)
        {
            var isExist = _repository.isExist(guid);
            if (!isExist)
            {
                return -1;
            }
            var edu = _repository.GetByGuid(guid);
            var deleteEdu = _repository.Delete(edu!);
            if (!deleteEdu)
            {
                return 0;
            }
            return 1;
        }
    }
}
