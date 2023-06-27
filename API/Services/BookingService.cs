using API.Contracts;
using API.DTOs.Booking;
using API.Models;

namespace API.Services
{
    public class BookingService
    {
        private readonly IBookingRepository _repository;

        public BookingService(IBookingRepository repository)
        {
            _repository = repository;
        }


        public IEnumerable<GetBookingDto>? GetBooking()
        {
            var emp = _repository.GetAll();
            if (!emp.Any())
            {
                return null;
            }

            // Ini Pake Linq buat mapping
            var toDto = emp.Select(b => new GetBookingDto
            {
                Guid = b.Guid,
                EmployeeGuid = b.EmployeeGuid,
                StartDate = b.StartDate,
                EndDate = b.EndDate,
                Remarks = b.Remarks,
                RoomGuid = b.RoomGuid,
                Status = b.Status,
            });

            return toDto;
        }

        public GetBookingDto? GetBookingGuid(Guid guid)
        {
            var book = _repository.GetByGuid(guid);
            if (book == null)
            {
                return null;
            }

            var toDto = new GetBookingDto
            {
                Guid = book.Guid,
                EmployeeGuid = book.EmployeeGuid,
                StartDate = book.StartDate,
                EndDate = book.EndDate,
                Remarks = book.Remarks,
                RoomGuid = book.RoomGuid,
                Status = book.Status,
            };

            return toDto;

        }

        public GetBookingDto? CreateBooking(NewBookingDto newBook)
        {
            var book = new Booking
            {
                Guid = new Guid(),
                CreatedDate = DateTime.Now,
                ModifiedDate = DateTime.Now,
                EmployeeGuid = newBook.EmployeeGuid,
                RoomGuid = newBook.RoomGuid,
                Status = newBook.Status,
                StartDate = newBook.StartDate,
                EndDate = newBook.EndDate,
                Remarks = newBook.Remarks,
            };

            var createdBooking = _repository.Create(book);
            if (createdBooking == null)
            {
                return null;
            }

            var toDto = new GetBookingDto
            {
                Guid = createdBooking.Guid,
                EmployeeGuid = createdBooking.EmployeeGuid,
                StartDate = createdBooking.StartDate,
                EndDate = createdBooking.EndDate,
                Remarks = createdBooking.Remarks,
                RoomGuid = createdBooking.RoomGuid,
                Status = createdBooking.Status
            };

            return toDto;

        }

        public int UpdateBooking(UpdateBookingDto updateBookDto)
        {
            var isExist = _repository.isExist(updateBookDto.Guid);
            if (!isExist)
            {
                return -1;
            }

            var getBook = _repository.GetByGuid(updateBookDto.Guid);

            var book = new Booking
            {
                Guid = updateBookDto.Guid,
                EmployeeGuid = updateBookDto.EmployeeGuid,
                Status = updateBookDto.Status,
                StartDate = updateBookDto.StartDate,
                EndDate = updateBookDto.EndDate,
                RoomGuid = updateBookDto.RoomGuid,
                Remarks = updateBookDto.Remarks,
                ModifiedDate = DateTime.Now,
                CreatedDate = getBook.CreatedDate
            };

            var isUpdate = _repository.Update(book);
            if (!isUpdate)
            {
                return 0;
            }
            return 1;
        }

        public int DeleteBooking(Guid guid)
        {
            var isExist = _repository.isExist(guid);
            if (!isExist)
            {
                return -1;
            }
            var book = _repository.GetByGuid(guid);
            var deleteBooking = _repository.Delete(book!);
            if (!deleteBooking)
            {
                return 0;
            }
            return 1;
        }
    }
}
