using RealEstateAPI.DTOs.UserDTos.Register;
using RealEstateAPI.DTOs.UserDTos.Login;
using RealEstateAPI.DTOs.UserDTos.Administration;
using RealEstateAPI.DTOs.UserDTos.ResetPassword;

using RealEstateAPI.Models;
using RealEstateAPI.Service.IServices;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Linq;
using System.Threading.Tasks;
using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace RealEstateAPI.Service.Services
{
    public class AuthService : IAuthService
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IConfiguration _configuration;
        private static readonly Dictionary<string, string> otpStorage = new Dictionary<string, string>();

        public AuthService(UserManager<ApplicationUser> userManager, IConfiguration configuration)
        {
            _userManager = userManager;
            _configuration = configuration;
        }

        #region Register
        public async Task<RegisterResultDTO> RegisterAsync(RegisterDTO model)
        {
            if (string.IsNullOrWhiteSpace(model.Email))
            {
                return new RegisterResultDTO { Message = "Error", Error = "Email is required." };
            }

            if (await _userManager.FindByEmailAsync(model.Email) != null)
            {
                return new RegisterResultDTO { Message = "Existing", Error = "Email is already registered." };
            }

            var user = new ApplicationUser
            {
                Email = model.Email,
                UserName = model.Email,
                PhoneNumber = model.PhoneNumber,
                IsActive = true
            };

            var result = await _userManager.CreateAsync(user, model.Password);
            if (!result.Succeeded)
            {
                string errors = string.Join(", ", result.Errors.Select(e => e.Description));
                return new RegisterResultDTO { Message = "Password", Error = errors };
            }

            return new RegisterResultDTO { Message = "Success" };
        }
        #endregion

        #region Login
        public async Task<LoginResponseDTO> Login(LoginDTO model)
        {
            var user = await _userManager.FindByEmailAsync(model.Email);
            if (user == null || !await _userManager.CheckPasswordAsync(user, model.Password))
            {
                return new LoginResponseDTO
                {
                    Email = "",
                    UserId = "",
                    Role = "",
                    Token = "",
                    Message = "Invalid Credentials",
                    IsAuthenticated = false,
                    Expier = DateTime.MinValue
                };
            }

            if (!user.IsActive)
            {
                return new LoginResponseDTO
                {
                    Email = user.Email,
                    UserId = user.Id,
                    Role = "",
                    Token = "",
                    Message = "Not Active!",
                    IsAuthenticated = false,
                    Expier = DateTime.MinValue
                };
            }

            var jwtToken = await CreateJwtToken(user);
            var roles = await _userManager.GetRolesAsync(user);

            return new LoginResponseDTO
            {
                Email = user.Email,
                UserId = user.Id,
                Role = roles.Contains("Admin") ? "Admin" : "User",
                Token = new JwtSecurityTokenHandler().WriteToken(jwtToken),
                Message = "Success",
                IsAuthenticated = true,
                Expier = jwtToken.ValidTo
            };
        }
        #endregion

        #region Forgot Password & OTP
        public async Task<bool> SendPasswordResetOTP(ResetPasswordRequestDTO model)
        {
            var user = await _userManager.Users.FirstOrDefaultAsync(u => u.PhoneNumber == model.PhoneNumber);
            if (user == null) return false;

            string otp = "1234"; 
            otpStorage[model.PhoneNumber] = otp;

            Console.WriteLine($"OTP for {model.PhoneNumber}: {otp}");

            return true;
        }

        public async Task<bool> ValidateOTP(VerifyOTPDTO model)
        {
            return otpStorage.ContainsValue(model.OTP); 
        }

        public async Task<bool> ResetPassword(ForgotPasswordDTO passwordModel)
        {
            var user = await _userManager.Users.FirstOrDefaultAsync();
            if (user == null) return false;

            if (passwordModel.NewPassword != passwordModel.ConfirmPassword)
                return false;

            var resetToken = await _userManager.GeneratePasswordResetTokenAsync(user);
            var result = await _userManager.ResetPasswordAsync(user, resetToken, passwordModel.NewPassword);

            if (!result.Succeeded)
            {
                foreach (var error in result.Errors)
                {
                    Console.WriteLine($"Reset Error: {error.Description}");  // Print error messages
                }
                return false;
            }

            return true;
        }





        #endregion

        #region List Users & User Activation
        public async Task<List<ListUsers>> ListOfUsers()
        {
            var users = _userManager.Users.ToList();
            var result = new List<ListUsers>();

            foreach (var user in users)
            {
                var roles = await _userManager.GetRolesAsync(user);
                result.Add(new ListUsers
                {
                    UserName = user.UserName,
                    Id = user.Id,
                    Phone = user.PhoneNumber,
                    Role = roles.FirstOrDefault() ?? "User"
                });
            }

            return result;
        }

        public async Task<string> UserActivation(UserActivation activation)
        {
            var user = await _userManager.FindByIdAsync(activation.UserId);
            if (user == null) return "Not Exist";

            if (!activation.IsActive)
            {
                await _userManager.DeleteAsync(user);
                return "Deleted";
            }

            user.IsActive = true;
            await _userManager.UpdateAsync(user);
            return "Success";
        }
        #endregion

        #region Get User By ID
        public async Task<ApplicationUser?> GetUserById(string userId)
        {
            return await _userManager.FindByIdAsync(userId);
        }
        #endregion

        #region Generate JWT Token
        private async Task<JwtSecurityToken> CreateJwtToken(ApplicationUser user)
        {
            var userClaims = await _userManager.GetClaimsAsync(user);
            var roles = await _userManager.GetRolesAsync(user);
            var roleClaims = roles.Select(role => new Claim("roles", role)).ToList();

            var claims = new List<Claim>
            {
                new(JwtRegisteredClaimNames.Sub, user.Email ?? ""),
                new(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                new("userId", user.Id)
            };

            claims.AddRange(userClaims);
            claims.AddRange(roleClaims);

            var secretKey = _configuration["JWT:SecretKey"];
            if (string.IsNullOrEmpty(secretKey))
            {
                throw new Exception("JWT SecretKey is missing in appsettings.json");
            }

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secretKey));
            var signingCredentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            return new JwtSecurityToken(
                claims: claims,
                issuer: _configuration["JWT:Issuer"],
                audience: _configuration["JWT:Audience"],
                expires: DateTime.UtcNow.AddDays(7),
                signingCredentials: signingCredentials
            );
        }
        #endregion
    }
}
