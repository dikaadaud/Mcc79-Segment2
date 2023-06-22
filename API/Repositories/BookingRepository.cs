using API.Contracts;
using API.Data;
using API.Models;

namespace API.Repositories
{
    public class BookingRepository : IBookingRepository
    {
        private readonly BookingDbContext _context;

        public BookingRepository(BookingDbContext context)
        {
            _context = context;
        }

        public Booking Create(Booking booking)
        {

            try
            {

                _context.Set<Booking>().Add(booking);
                _context.SaveChanges();
                return booking;
            }
            catch (Exception)
            {
                return new Booking();
            }


        }

        public bool Delete(Guid guid)
        {
            var booking = GetByGuid(guid);
            if (booking is null)
            {
                return false;
            }
            try
            {
                _context.Set<Booking>().Remove(booking);
                _context.SaveChanges();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public ICollection<Booking> GetAll()
        {
            return _context.Set<Booking>().ToList();
        }

        public Booking? GetByGuid(Guid guid)
        {
            return _context.Set<Booking>().Find(guid);
        }

        public bool Update(Booking booking)
        {
            try
            {
                _context.Set<Booking>().Update(booking);
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
