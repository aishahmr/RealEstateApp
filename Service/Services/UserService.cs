using Microsoft.AspNetCore.Identity;
using RealEstateAPI.DTOs.UserDTos.UserManagement;
using RealEstateAPI.Models;
using RealEstateAPI.Service.IServices; // Ensure this is included
using System.Threading.Tasks;

namespace RealEstateAPI.Service.Services
{
    public class UserService : IUserService  // Implement IUserService
    {
        private readonly UserManager<ApplicationUser> _userManager;

        public UserService(UserManager<ApplicationUser> userManager)
        {
            _userManager = userManager;
        }

        public Task<bool> ApproveProperty(Guid propertyId)
        {
            throw new NotImplementedException();
        }

        public Task<bool> BlockUser(string userId)
        {
            throw new NotImplementedException();
        }

        public Task<bool> DeleteUser(string userId)
        {
            throw new NotImplementedException();
        }

        public Task<List<UserResponseDTO>> GetAllUsers()
        {
            throw new NotImplementedException();
        }

        public async Task<ApplicationUser> GetUserByIdAsync(string userId)
        {
            return await _userManager.FindByIdAsync(userId);
        }

        public Task<UserDetailsDTO> GetUserDetails(string userId)
        {
            throw new NotImplementedException();
        }

        public Task<bool> UnblockUser(string userId)
        {
            throw new NotImplementedException();
        }

        public async Task<bool> UpdateUserAsync(ApplicationUser user)
        {
            var result = await _userManager.UpdateAsync(user);
            return result.Succeeded;
        }

        public Task<bool> UpdateUserProfile(string userId, UpdateUserDTO model)
        {
            throw new NotImplementedException();
        }
    }
}

