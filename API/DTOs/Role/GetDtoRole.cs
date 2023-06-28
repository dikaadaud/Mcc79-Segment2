using System.ComponentModel.DataAnnotations;

namespace API.DTOs.Role
{
    public class GetDtoRole
    {
        [Required]
        public Guid Guid { get; set; }
        [Required]
        public string Name { get; set; }

    }
}
