namespace API.DTOs.AccountRole
{
    public class RegisterAccountRole
    {
        public Guid AccountGuid { get; set; }
        public Guid RoomGuid { get; set; }
        public string RoleName { get; set; }
    }
}
