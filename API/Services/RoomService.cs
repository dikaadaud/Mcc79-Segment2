﻿using API.Contracts;
using API.DTOs.Room;
using API.Models;
using API.Ultilities.Enum;

namespace API.Services
{
    public class RoomService
    {
        private readonly IRoom _repository;
        private readonly IBookingRepository _bookingRepository;

        public RoomService(IRoom repository, IBookingRepository bookingRepository)
        {
            _repository = repository;
            _bookingRepository = bookingRepository;
        }

        public IEnumerable<GetRoomDto>? GetRoom()
        {
            var rooms = _repository.GetAll();
            if (!rooms.Any())
            {
                return null;
            }

            // Ini Pake Linq buat mapping
            var toDto = rooms.Select(r => new GetRoomDto
            {
                Guid = r.Guid,
                Name = r.Name,
                floor = r.Floor,
                Capacity = r.Capacity,
            });

            return toDto;
        }

        public GetRoomDto? GetRoomGuid(Guid guid)
        {
            var rooms = _repository.GetByGuid(guid);
            if (rooms == null)
            {
                return null;
            }

            var toDto = new GetRoomDto
            {
                Guid = rooms.Guid,
                Name = rooms.Name,
                floor = rooms.Floor,
                Capacity = rooms.Capacity,
            };

            return toDto;

        }

        public GetRoomDto? CreateRole(NewRoomDto newRoom)
        {
            var rooms = new Room
            {
                Name = newRoom.Name,
                Floor = newRoom.Floor,
                Capacity = newRoom.Capacity,
                Guid = new Guid(),
                CreatedDate = DateTime.Now,
                ModifiedDate = DateTime.Now,
            };

            var createdRoom = _repository.Create(rooms);
            if (createdRoom == null)
            {
                return null;
            }

            var toDto = new GetRoomDto
            {
                Guid = createdRoom.Guid,
                Name = createdRoom.Name,
                floor = createdRoom.Floor,
                Capacity = createdRoom.Capacity,
            };

            return toDto;

        }

        public int UpdateRoom(UpdateRoomDto updateRoomDto)
        {
            var isExist = _repository.isExist(updateRoomDto.Guid);
            if (!isExist)
            {
                return -1;
            }

            var getRooms = _repository.GetByGuid(updateRoomDto.Guid);

            var rooms = new Room
            {
                Name = updateRoomDto.Name,
                Guid = updateRoomDto.Guid,
                Floor = updateRoomDto.floor,
                Capacity = updateRoomDto.Capacity,
                ModifiedDate = DateTime.Now,
                CreatedDate = getRooms.CreatedDate
            };

            var isUpdate = _repository.Update(rooms);
            if (!isUpdate)
            {
                return 0;
            }
            return 1;
        }

        public int DeleteRoom(Guid guid)
        {
            var isExist = _repository.isExist(guid);
            if (!isExist)
            {
                return -1;
            }
            var rooms = _repository.GetByGuid(guid);
            var deleteRoom = _repository.Delete(rooms!);
            if (!deleteRoom)
            {
                return 0;
            }
            return 1;
        }

        public IEnumerable<RoomNotUseDto>? GetRoomNotUse()
        {
            var rooms = _repository.GetAll().ToList();

            if (rooms is null)
            {
                return null;
            }

            var bookings = _bookingRepository.GetAll().ToList();
            if (bookings is null)
            {
                return null;
            }
            var detailsRooms = from r in _repository.GetAll()
                               join b in _bookingRepository.GetAll()
                               on r.Guid equals b.RoomGuid
                               where b.Status == StatusLevel.OnGoing
                               select new GetRoomDto
                               {
                                   Guid = r.Guid,
                                   Capacity = r.Capacity,
                                   floor = r.Floor,
                                   Name = r.Name,

                               };

            List<Room> tmpRoom = new List<Room>(rooms);

            foreach (var r in rooms)
            {
                foreach (var usedRoom in detailsRooms)
                {
                    if (r.Guid == usedRoom.Guid)
                    {
                        tmpRoom.Remove(r);
                        break;
                    }
                }
            }

            var unUsed = from r in tmpRoom
                         select (new RoomNotUseDto
                         {
                             RoomGuid = r.Guid,
                             RoomName = r.Name,
                             Capacity = r.Capacity,
                             Floor = r.Floor,
                         });

            return unUsed;
        }
    }
}
