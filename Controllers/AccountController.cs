using RealEstateAPI.DTOs.UserDTos.Login;
using RealEstateAPI.DTOs.UserDTos.Register;
using RealEstateAPI.DTOs.UserDTos.ResetPassword; //  Import DTO for password reset
using RealEstateAPI.Service.IServices;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace RealEstateAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        #region Fields
        private readonly IAuthService _authService;
        #endregion

        #region Ctor
        public AccountController(IAuthService authService)
        {
            _authService = authService;
        }
        #endregion

        #region Handle Functions
        [HttpPost("[action]")]
        public async Task<IActionResult> Register([FromForm] RegisterDTO DTO)
        {
            var result = await _authService.RegisterAsync(DTO);
            if (result.Message == "Existing") return BadRequest(new { code = result.Message, message = result.Error });
            if (result.Message == "Password") return BadRequest(new { code = result.Message, message = result.Error });
            if (result.Message == "Image") return BadRequest(new { code = result.Message, message = result.Error });
            return Ok();
        }

        [HttpPost("[action]")]
        public async Task<IActionResult> Login(LoginDTO DTO)
        {
            var result = await _authService.Login(DTO);
            if (!result.IsAuthenticated) return BadRequest(result.Message);
            return Ok(result);
        }

        // 📌 NEW: Forgot Password (Sends OTP)
        [HttpPost("ForgotPassword")]
        public async Task<IActionResult> ForgotPassword([FromBody] ForgotPasswordDTO model)
        {
            var result = await _authService.SendPasswordResetOTP(model.Email);
            if (!result) return BadRequest("Failed to send OTP. Email may not be registered.");
            return Ok("OTP sent successfully.");
        }

        // 📌 NEW: Verify OTP
        [HttpPost("VerifyOTP")]
        public async Task<IActionResult> VerifyOTP([FromBody] VerifyOTPDTO model)
        {
            var isValid = await _authService.ValidateOTP(model.Email, model.OTP);
            if (!isValid) return BadRequest("Invalid OTP.");
            return Ok("OTP verified. You can now reset your password.");
        }

        // 📌 NEW: Reset Password
        [HttpPost("ResetPassword")]
        public async Task<IActionResult> ResetPassword([FromBody] ResetPasswordDTO model)
        {
            var result = await _authService.ResetPassword(model.Email, model.OTP, model.NewPassword);
            if (!result) return BadRequest("Failed to reset password. Please check your OTP and try again.");
            return Ok("Password reset successfully.");
        }
        #endregion
    }
}
