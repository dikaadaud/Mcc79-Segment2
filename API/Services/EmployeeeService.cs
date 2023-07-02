using API.Contracts;
using API.DTOs.Employee;
using API.Models;

namespace API.Services
{
    public class EmployeeeService
    {
        private readonly IEmployeeRepository _repository;
        private readonly IEducationRepository _educationRepository;
        private readonly IUniversityRepository _universityRepository;
        private IEmployeeRepository employeeRepository;

        public EmployeeeService(IEmployeeRepository repository, IEducationRepository educationRepository, IUniversityRepository universityRepository)
        {
            _repository = repository;
            _educationRepository = educationRepository;
            _universityRepository = universityRepository;
        }

        public EmployeeeService(IEmployeeRepository employeeRepository)
        {
            this.employeeRepository = employeeRepository;
        }

        public IEnumerable<GetEmployeeDto>? GetEmployee()
        {
            var emp = _repository.GetAll();
            if (!emp.Any())
            {
                return null;
            }

            // Ini Pake Linq buat mapping
            var toDto = emp.Select(acc => new GetEmployeeDto
            {
                Guid = acc.Guid,
                BirthDate = acc.BirthDate,
                FirstName = acc.FirstName,
                LastName = acc.LastName,
                Email = acc.Email,
                PhoneNumber = acc.PhoneNumber,
                Nik = acc.Nik,
                Gender = acc.Gender,
                HiringDate = acc.HiringDate
            });

            return toDto;
        }

        public GetEmployeeDto? GetEmployeeGuid(Guid guid)
        {
            var emp = _repository.GetByGuid(guid);
            if (emp == null)
            {
                return null;
            }

            var toDto = new GetEmployeeDto
            {
                Guid = emp.Guid,
                BirthDate = emp.BirthDate,
                FirstName = emp.FirstName,
                LastName = emp.LastName,
                Email = emp.Email,
                PhoneNumber = emp.PhoneNumber,
                Nik = emp.Nik,
                Gender = emp.Gender,
                HiringDate = emp.HiringDate
            };

            return toDto;

        }

        public GetEmployeeDto? CreateEmployee(NewEmployee newEmployee)
        {
            var emp = new Employee
            {
                Guid = new Guid(),
                CreatedDate = DateTime.Now,
                ModifiedDate = DateTime.Now,
                Nik = GenerateNIK(),
                Gender = newEmployee.Gender,
                FirstName = newEmployee.FirstName,
                LastName = newEmployee.LastName,
                Email = newEmployee.Email,
                PhoneNumber = newEmployee.PhoneNumber,
                HiringDate = DateTime.Now,
                BirthDate = newEmployee.BirthDate,

            };

            var createdEmployee = _repository.Create(emp);
            if (createdEmployee == null)
            {
                return null;
            }

            var toDto = new GetEmployeeDto
            {
                Guid = createdEmployee.Guid,
                Email = createdEmployee.Email,
                PhoneNumber = createdEmployee.PhoneNumber,
                FirstName = createdEmployee.FirstName,
                LastName = createdEmployee.LastName,
                Nik = createdEmployee.Nik,
                Gender = createdEmployee.Gender,
                BirthDate = createdEmployee.BirthDate
            };

            return toDto;

        }

        public int UpdateEmployee(UpdateEmployeeDto updateEmpDto)
        {
            var isExist = _repository.isExist(updateEmpDto.Guid);
            if (!isExist)
            {
                return -1;
            }

            var getEmp = _repository.GetByGuid(updateEmpDto.Guid);

            var emp = new Employee
            {
                Guid = updateEmpDto.Guid,
                FirstName = updateEmpDto.FirstName,
                Nik = updateEmpDto.Nik,
                LastName = updateEmpDto.LastName,
                Gender = updateEmpDto.Gender,
                BirthDate = updateEmpDto.BirthDate,
                Email = updateEmpDto.Email,
                PhoneNumber = updateEmpDto.PhoneNumber,
                ModifiedDate = DateTime.Now,
                CreatedDate = getEmp!.CreatedDate,
                HiringDate = updateEmpDto.HiringDate

            };

            var isUpdate = _repository.Update(emp);
            if (!isUpdate)
            {
                return 0;
            }
            return 1;
        }

        public int DeleteEmployee(Guid guid)
        {
            var isExist = _repository.isExist(guid);
            if (!isExist)
            {
                return -1;
            }
            var emp = _repository.GetByGuid(guid);
            var deleteEmp = _repository.Delete(emp!);
            if (!deleteEmp)
            {
                return 0;
            }
            return 1;
        }

        public string GenerateNIK()
        {
            var getEmpo = _repository.GetAll();

            if (getEmpo.Count == 0)
            {
                return $"AT/{getEmpo.Count + 1}";

            }
            else
            {
                var lastData = getEmpo.LastOrDefault();

                string lastNik = lastData.Nik;
                int lastNUmber = int.Parse(lastNik.Substring(3));
                string newnik = "AT/" + (lastNUmber + 1);
                return newnik;
            }
        }

        public IEnumerable<EmployeEducationDto> GetMaster()
        {
            var master = from e in _repository.GetAll()
                         join edu in _educationRepository.GetAll() on e.Guid equals edu.Guid
                         join u in _universityRepository.GetAll() on edu.UniversityGuid equals u.Guid
                         select (new EmployeEducationDto
                         {
                             Guid = e.Guid,
                             Fullname = e.FirstName + e.LastName,
                             BirthDate = e.BirthDate,
                             Email = e.Email,
                             Phone = e.PhoneNumber,
                             Gender = e.Gender,
                             Nik = e.Nik,
                             HiringDate = e.HiringDate,
                             Degree = edu.Degree,
                             Gpa = edu.Gpa,
                             Major = edu.Major,
                             UniversityName = u.Name
                         });

            return master;
        }

        public EmployeEducationDto? GetMasterByGuid(Guid guid)
        {
            var getMaster = GetMaster();

            var getMasterByGuid = getMaster.FirstOrDefault(m => m.Guid == guid);

            return getMasterByGuid;
        }

    }
}
