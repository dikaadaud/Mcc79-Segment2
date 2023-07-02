﻿using API.Contracts;
using API.DTOs.Account;
using API.Models;
using API.Ultilities.Enum;
using System.Security.Claims;

namespace API.Services
{
    public class AccountService
    {
        private readonly IAccountRepository _repository;
        private readonly IEmployeeRepository _employeeRepository;
        private readonly IEducationRepository _educationRepository;
        private readonly IUniversityRepository _universityRepository;
        private readonly ITokenHandler _tokenHandler;
        private readonly IEmailHandler _emailHandler;

        public AccountService(IAccountRepository repository, IEmployeeRepository employeeRepository, IEducationRepository educationRepository, IUniversityRepository universityRepository, ITokenHandler tokenHandler, IEmailHandler emailHandler)
        {
            _repository = repository;
            _employeeRepository = employeeRepository;
            _educationRepository = educationRepository;
            _universityRepository = universityRepository;
            _tokenHandler = tokenHandler;
            _emailHandler = emailHandler;
        }

        public IEnumerable<GetAccountDto>? GetAccount()
        {
            var acc = _repository.GetAll();
            if (!acc.Any())
            {
                return null;
            }

            // Ini Pake Linq buat mapping
            var toDto = acc.Select(a => new GetAccountDto
            {
                Guid = a.Guid,
                ExpiredTime = a.ExpriedTime,
                IsDeleted = a.IsDeleted,
                IsUsed = a.IsUsed,
                Otp = a.Otp,
                Password = a.Password,

            });

            return toDto;
        }

        public GetAccountDto? GetAccGuid(Guid guid)
        {
            var acc = _repository.GetByGuid(guid);
            if (acc == null)
            {
                return null;
            }

            var toDto = new GetAccountDto
            {
                Guid = acc.Guid,
                ExpiredTime = acc.ExpriedTime,
                IsDeleted = acc.IsDeleted,
                IsUsed = acc.IsUsed,
                Otp = acc.Otp,
                Password = acc.Password
            };

            return toDto;

        }

        public GetAccountDto? CreateAccount(NewAccountDto newAccount)
        {
            var acc = new Account
            {
                Guid = newAccount.Guid,
                CreatedDate = DateTime.Now,
                ModifiedDate = DateTime.Now,
                ExpriedTime = newAccount.ExpiredTime,
                IsDeleted = newAccount.IsDeleted,
                IsUsed = newAccount.IsUsed,
                Password = Hashing.HashPassword(newAccount.Password)

            };

            var createdAccount = _repository.Create(acc);
            if (createdAccount == null)
            {
                return null;
            }

            var toDto = new GetAccountDto
            {
                Guid = createdAccount.Guid,
                ExpiredTime = createdAccount.ExpriedTime,
                IsDeleted = createdAccount.IsDeleted,
                IsUsed = createdAccount.IsUsed,
                Password = createdAccount.Password
            };

            return toDto;

        }

        public int UpdateAccount(UpdateAccountDto updateAccDto)
        {
            var isExist = _repository.isExist(updateAccDto.Guid);
            if (!isExist)
            {
                return -1;
            }

            var getAcc = _repository.GetByGuid(updateAccDto.Guid);

            var acc = new Account
            {
                Guid = updateAccDto.Guid,
                ExpriedTime = getAcc.ExpriedTime,
                IsDeleted = updateAccDto.IsDeleted,
                IsUsed = updateAccDto.IsUsed,
                Otp = getAcc.Otp,
                Password = Hashing.HashPassword(updateAccDto.Password),
                CreatedDate = getAcc.CreatedDate,
                ModifiedDate = DateTime.Now

            };

            var isUpdate = _repository.Update(acc);
            if (!isUpdate)
            {
                return 0;
            }
            return 1;
        }


        public RegisterDto? RegisterAccount(RegisterDto registerDto)
        {
            EmployeeeService employeeeService = new EmployeeeService(_employeeRepository);

            var emp = new Employee
            {
                Guid = new Guid(),
                Nik = employeeeService.GenerateNIK(),
                FirstName = registerDto.FirstName,
                LastName = registerDto.LastName,
                BirthDate = registerDto.BirthDate,
                HiringDate = registerDto.HiringDate,
                Email = registerDto.Email,
                PhoneNumber = registerDto.PhoneNumber,
                Gender = registerDto.Gender,
                CreatedDate = DateTime.Now,
                ModifiedDate = DateTime.Now
            };

            var createEmp = _employeeRepository.Create(emp);
            if (createEmp == null)
            {
                return null;
            }

            var acc = new Account
            {
                Guid = emp.Guid,
                Password = Hashing.HashPassword(registerDto.Password),
            };

            var createdAcc = _repository.Create(acc);
            if (createdAcc == null)
            {
                return null;
            }


            var univ = new University
            {
                Guid = new Guid(),
                Code = registerDto.UniversityCode,
                Name = registerDto.UniversityName
            };

            var createUniv = _universityRepository.Create(univ);
            if (createUniv == null)
            {
                return null;
            }


            var edu = new Education
            {
                Guid = emp.Guid,
                Degree = registerDto.Degree,
                Gpa = registerDto.GPA,
                Major = registerDto.Major,
                UniversityGuid = univ.Guid
            };

            var createEdu = _educationRepository.Create(edu);
            if (createEdu == null)
            {
                return null;
            }

            var toDto = new RegisterDto
            {
                FirstName = createEmp.FirstName,
                LastName = createEmp.LastName ?? "",
                BirthDate = createEmp.BirthDate,
                Email = createEmp.Email,
                PhoneNumber = createEmp.PhoneNumber,
                Gender = createEmp.Gender,
                Password = createdAcc.Password,
                ConfirmPassword = createdAcc.Password,
                GPA = createEdu.Gpa,
                Degree = createEdu.Degree,
                HiringDate = createEmp.HiringDate,
                Major = createEdu.Major,
                UniversityCode = createUniv.Code,
                UniversityName = createUniv.Name
            };

            return toDto;
        }

        public int DeleteAcc(Guid guid)
        {
            var isExist = _repository.isExist(guid);
            if (!isExist)
            {
                return -1;
            }
            var acc = _repository.GetByGuid(guid);
            var deleteAcc = _repository.Delete(acc!);
            if (!deleteAcc)
            {
                return 0;
            }
            return 1;
        }

        public string Login(Login login)
        {
            var emailEmp = _employeeRepository.GetEmail(login.Email);
            if (emailEmp == null)
            {
                return "0";
            }

            var pass = _repository.GetByGuid(emailEmp.Guid);
            var isValid = Hashing.ValidatePassword(login.Password, pass!.Password);
            if (!isValid)
            {
                return "-1";
            }

            var claims = new List<Claim>()
            {
                new Claim("Nik",emailEmp.Nik),
                new Claim("Fullname", $"{emailEmp.FirstName} {emailEmp.LastName}"),
                new Claim("Email",emailEmp.Email)
            };


            try
            {
                var getToken = _tokenHandler.GenerateToken(claims);
                return getToken;
            }
            catch (Exception)
            {

                return "-2";
            }
        }

        public int ForgotPassword(ForgotPasswordDto forgotPassword)
        {
            var employee = _employeeRepository.GetEmail(forgotPassword.Email);
            if (employee is null)
                return 0; // Email not found

            var account = _repository.GetByGuid(employee.Guid);
            if (account is null)
                return -1;

            var otp = new Random().Next(111111, 999999);
            var isUpdated = _repository.Update(new Account
            {
                Guid = account.Guid,
                Password = account.Password,
                IsDeleted = account.IsDeleted,
                Otp = otp,
                ExpriedTime = DateTime.Now.AddMinutes(5),
                IsUsed = false,
                CreatedDate = account.CreatedDate,
                ModifiedDate = DateTime.Now
            });
            if (!isUpdated)
                return -1;

            _emailHandler.sendEmail(forgotPassword.Email,
                                    "Forgot Password",
                                    $"Your OTP is {otp}");

            return 1;
        }

        public int ChangePassword(ChangePasswordDto changePassword)
        {

            var isExist = _employeeRepository.getEmailandPhone(changePassword.Email);
            if (isExist == null)
            {
                return -1;
            }
            var getAccount = _repository.GetByGuid(isExist.Guid);
            if (getAccount.Otp != changePassword.Otp)
            {
                return 0;
            }
            if (getAccount.IsUsed == true)
            {
                return 1;
            }
            if (getAccount.ExpriedTime < DateTime.Now)
            {
                return 2;
            }
            var account = new Account
            {
                Guid = getAccount.Guid,
                IsUsed = getAccount.IsUsed,
                IsDeleted = getAccount.IsDeleted,
                ModifiedDate = DateTime.Now,
                CreatedDate = getAccount.CreatedDate,
                Otp = getAccount.Otp,
                ExpriedTime = getAccount.ExpriedTime,
                Password = Hashing.HashPassword(changePassword.NewPassword)
            };
            var isUpdate = _repository.Update(account);
            if (!isUpdate)
            {
                return 0;
            }


            return 3;
        }

    }
}
