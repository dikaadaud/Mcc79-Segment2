using API.Contracts;
using API.Data;
using API.Models;

namespace API.Repositories
{
    public class EducationRepository : IEducationRepository
    {
        private readonly BookingDbContext _context;

        public EducationRepository(BookingDbContext context)
        {
            _context = context;
        }

        public Education Create(Education education)
        {
            try
            {

                _context.Set<Education>().Add(education);
                _context.SaveChanges();
                return education;
            }
            catch (Exception)
            {
                return new Education();
            }
        }

        public bool Delete(Guid guid)
        {
            var education = GetByGuid(guid);
            if (education is null)
            {
                return false;
            }
            try
            {
                _context.Set<Education>().Remove(education);
                _context.SaveChanges();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public ICollection<Education> GetAll()
        {
            return _context.Set<Education>().ToList();
        }

        public Education? GetByGuid(Guid guid)
        {
            return _context.Set<Education>().Find(guid);
        }

        public bool Update(Education education)
        {
            try
            {
                _context.Set<Education>().Update(education);
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
