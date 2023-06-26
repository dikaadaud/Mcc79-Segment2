using API.Contracts;
using API.Data;

namespace API.Repositories
{
    public class GeneralRepository<TEntity> : IGeneralRepository<TEntity>
        where TEntity : class
    {
        protected readonly BookingDbContext _context;

        public GeneralRepository(BookingDbContext context)
        {
            _context = context;
        }

        public TEntity? Create(TEntity entity)
        {
            try
            {

                _context.Set<TEntity>().Add(entity);
                _context.SaveChanges();
                return entity;
            }
            catch (Exception)
            {
                return null;
            }
        }

        public bool Delete(TEntity entities)
        {
            try
            {
                _context.Set<TEntity>().Remove(entities);
                _context.SaveChanges();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public ICollection<TEntity> GetAll()
        {
            return _context.Set<TEntity>().ToList();
        }

        public TEntity? GetByGuid(Guid guid)
        {

            var entity = _context.Set<TEntity>().Find(guid);
            _context.ChangeTracker.Clear();
            return entity;
        }

        public bool Update(TEntity entity)
        {
            try
            {
                _context.Set<TEntity>().Update(entity);
                _context.SaveChanges();
                return true;
            }
            catch (Exception)
            {
                return false;
                throw;
            }
        }

        public bool isExist(Guid guid)
        {

            return GetByGuid(guid) is not null;
        }
    }
}
