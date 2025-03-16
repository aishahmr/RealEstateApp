using RealEstateAPI.DTOs.UserDTos.Login;
using RealEstateAPI.DTOs.UserDTos.Register;
using RealEstateAPI.DTOs.UserDTos.Administration;
using RealEstateAPI.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace RealEstateAPI.Service.IServices
{
    public interface IAuthService
    {
        Task<RegisterResultDTO> RegisterAsync(RegisterDTO model);
        Task<LoginResponseDTO> Login(LoginDTO model);
        Task<List<ListUsers>> ListOfUsers();
        Task<string> UserActivation(UserActivation activation);
        Task<ApplicationUser> GetUserById(string userId);

      
        Task<bool> SendPasswordResetOTP(string email);
        Task<bool> ValidateOTP(string email, string otp);
        Task<bool> ResetPassword(string email, string otp, string newPassword);
    }
}


