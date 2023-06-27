using System.ComponentModel.DataAnnotations;

namespace API.DTOs.AccountRole
{
    public class NewAccountRole
    {
        [Required(ErrorMessage = "Harus Masukan Account Guid")]
        public Guid AccountGuid { get; set; }
        [Required(ErrorMessage = "Harus Masukan Role Guid")]
        public Guid RoleGuid { get; set; }
    }
}
