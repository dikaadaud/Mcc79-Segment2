﻿using API.Contracts;
using API.Data;
using API.Models;

namespace API.Repositories
{
    public class AccountRepository : IAccountRepository
    {
        private readonly BookingDbContext _context;

        public AccountRepository(BookingDbContext context)
        {
            _context = context;
        }
        public Account Create(Account account)
        {
            try
            {

                _context.Set<Account>().Add(account);
                _context.SaveChanges();
                return account;
            }
            catch (Exception)
            {
                return new Account();
            }
        }

        public bool Delete(Guid guid)
        {
            var account = GetByGuid(guid);
            if (account is null)
            {
                return false;
            }
            try
            {
                _context.Set<Account>().Remove(account);
                _context.SaveChanges();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public ICollection<Account> GetAll()
        {
            return _context.Set<Account>().ToList();
        }

        public Account? GetByGuid(Guid guid)
        {
            return _context.Set<Account>().Find(guid);
        }

        public bool Update(Account account)
        {
            try
            {
                _context.Set<Account>().Update(account);
                _context.SaveChanges();
                return true;
            }
            catch (Exception)
            {
                return false;
                throw;
            }
        }
    }
}
