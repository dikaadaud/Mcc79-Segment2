using API.Contracts;
using API.DTOs.Account;
using API.Models;
using API.Ultilities.Enum;

namespace API.Services
{
    public class AccountService
    {
        private readonly IAccountRepository _repository;

        public AccountService(IAccountRepository repository)
        {
            _repository = repository;
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
    }
}
