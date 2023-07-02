namespace API.DTOs.Room
{
    public class RoomNotUseDto
    {
        public string RoomName { get; set; }
        public Guid RoomGuid { get; set; }
        public int Floor { get; set; }
        public int Capacity { get; set; }
    }
}
