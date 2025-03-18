using System.ComponentModel.DataAnnotations;

namespace RealEstateAPI.DTOs.UserDTos.ResetPassword
{
    public class VerifyOTPDTO
    {
        [StringLength(4, MinimumLength = 4)]
        public string OTP { get; set; }
    }
}

