using RealEstateAPI.DTOs.UserDTos.Login;
using RealEstateAPI.DTOs.UserDTos.Register;
using RealEstateAPI.DTOs.UserDTos.ResetPassword; 

using RealEstateAPI.Service.IServices;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

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

       
        [HttpPost("ForgotPassword")]
        public async Task<IActionResult> ForgotPassword([FromBody] ResetPasswordRequestDTO model) 
        {
            var result = await _authService.SendPasswordResetOTP(model);
            if (!result) return BadRequest("Failed to send OTP. Phone number may not be registered.");
            return Ok("OTP sent successfully.");
        }


        [HttpPost("VerifyOTP")]
        public async Task<IActionResult> VerifyOTP([FromBody] VerifyOTPDTO model)
        {
            var isValid = await _authService.ValidateOTP(model); 

            if (!isValid) return BadRequest("Invalid OTP.");
            return Ok("OTP verified. You can now reset your password.");
        }






        [HttpPost("ResetPassword")]
        public async Task<IActionResult> ResetPassword([FromBody] ForgotPasswordDTO passwordModel)
        {
            var result = await _authService.ResetPassword(passwordModel);
            if (!result) return BadRequest("Failed to reset password. Please try again.");

            return Ok("Password reset successfully.");
        }




        #endregion
    }
}
