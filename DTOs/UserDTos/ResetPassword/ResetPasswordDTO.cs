namespace RealEstateAPI.DTOs.UserDTos.ResetPassword
{
    public class ResetPasswordDTO
    {
        public string Email { get; set; }
        public string OTP { get; set; }
        public string NewPassword { get; set; }
    }
}
