using API.Contracts;
using API.DTOs.Booking;
using API.Models;

namespace API.Services
{
    public class BookingService
    {
        private readonly IBookingRepository _repository;
        private readonly IEmployeeRepository _employeeRepository;
        private readonly IRoom _roomRepository;

        public BookingService(IBookingRepository repository, IEmployeeRepository employeeRepository, IRoom roomRepository)
        {
            _repository = repository;
            _employeeRepository = employeeRepository;
            _roomRepository = roomRepository;
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

        public IEnumerable<DetailsBooking>? BookingDetail()
        {
            var books = (from b in _repository.GetAll()
                         join e in _employeeRepository.GetAll() on b.EmployeeGuid equals e.Guid
                         join r in _roomRepository.GetAll() on b.RoomGuid equals r.Guid
                         select new DetailsBooking
                         {
                             Guid = b.Guid,
                             BookedNik = e.Nik,
                             BookedBy = e.FirstName + " " + e.LastName,
                             StartDate = DateTime.Now,
                             EndDate = b.EndDate,
                             RoomName = r.Name,
                             Status = b.Status,
                             Remarks = b.Remarks,
                         }).ToList();
            if (!books.Any())
            {
                return null;
            }

            return books;
        }

        public DetailsBooking? BookingDetail(Guid guid)
        {
            var books = BookingDetail();

            var bookByGuid = books!.FirstOrDefault(book => book.Guid == guid);

            return bookByGuid;

        }

        public IEnumerable<BookingDetailsDto?> BookingRoomDetail()
        {
            var books = _repository.GetAll();
            if (books == null)
            {
                return null;
            }

            var detailBooking = from e in _employeeRepository.GetAll()
                                join b in _repository.GetAll() on e.Guid equals b.EmployeeGuid
                                join r in _roomRepository.GetAll() on b.RoomGuid equals r.Guid
                                where b.StartDate <= DateTime.Now && b.EndDate >= DateTime.Now
                                select (new BookingDetailsDto
                                {
                                    BookedBy = e.FirstName + " " + e.LastName,
                                    BookingGuid = b.Guid,
                                    Floor = r.Floor,
                                    RoomName = r.Name,
                                    Status = b.Status
                                });
            return detailBooking;
        }

        public IEnumerable<BookingLenghtDto> BookingDuration()
        {
            var books = _repository.GetAll();
            if (books == null)
            {
                return null;
            }

            var entities = (from b in books
                            join r in _roomRepository.GetAll() on b.RoomGuid equals r.Guid
                            select new
                            {
                                guid = r.Guid,
                                startDate = b.StartDate,
                                endDate = b.EndDate,
                                roomName = r.Name
                            }).ToList();
            var bookingDurations = new List<BookingLenghtDto>();
            foreach (var b in entities)
            {
                TimeSpan duration = b.endDate - b.startDate;
                int totalDays = (int)duration.TotalDays;
                int weekend = 0;
                for (int i = 0; i < totalDays; i++)
                {
                    var currentDate = b.startDate.AddDays(i);
                    if (currentDate.DayOfWeek == DayOfWeek.Saturday || currentDate.DayOfWeek == DayOfWeek.Sunday)
                    {
                        weekend++;
                    }
                }

                TimeSpan bookingLength = duration - TimeSpan.FromDays(weekend);

                var bookingDuration = new BookingLenghtDto()
                {
                    RoomGuid = b.guid,
                    RoomName = b.roomName,
                    BookingLength = bookingLength
                };
                bookingDurations.Add(bookingDuration);
            }
            return bookingDurations;
        }
    }
}
