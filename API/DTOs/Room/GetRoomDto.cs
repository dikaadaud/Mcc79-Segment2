using System.ComponentModel.DataAnnotations;

namespace API.DTOs.Room
{
    public class GetRoomDto
    {
        [Required]
        public Guid Guid { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public int Capacity { get; set; }
        [Required]
        public int floor { get; set; }

    }
}
