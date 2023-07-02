namespace API.DTOs.Account
{
    public class ForgotPasswordDto
    {
        public Guid Guid { get; set; }
        public string Email { get; set; }
        public int OTP { get; set; }
    }
}
