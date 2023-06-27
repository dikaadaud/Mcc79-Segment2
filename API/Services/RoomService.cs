using API.Contracts;
using API.DTOs.Room;
using API.Models;

namespace API.Services
{
    public class RoomService
    {
        private readonly IRoom _repository;

        public RoomService(IRoom repository)
        {
            _repository = repository;
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
    }
}
