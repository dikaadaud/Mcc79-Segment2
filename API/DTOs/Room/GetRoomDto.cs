namespace API.DTOs.Room
{
    public class GetRoomDto
    {
        public Guid Guid { get; set; }
        public string Name { get; set; }
        public int Capacity { get; set; }
        public int floor { get; set; }

    }
}
