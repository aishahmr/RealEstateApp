namespace RealEstateAPI.DTOs.UserDTos.ResetPassword
{
    public class ForgotPasswordDTO
    {
        public string Email { get; set; }
        public string NewPassword { get; set; }
        public string ConfirmPassword { get; set; }
    }
}
