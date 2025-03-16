using System.Collections.Generic;
using System.Threading.Tasks;
using RealEstateAPI.DTOs.UserDTos.UserManagement;
using System;

namespace RealEstateAPI.Service.IServices
{
    public interface IUserService
    {
        Task<List<UserResponseDTO>> GetAllUsers();
        Task<UserDetailsDTO> GetUserDetails(string userId);
        Task<bool> UpdateUserProfile(string userId, UpdateUserDTO model);
        Task<bool> DeleteUser(string userId);

        // Added new methods for admin actions
        Task<bool> ApproveProperty(Guid propertyId);
        Task<bool> BlockUser(string userId);
        Task<bool> UnblockUser(string userId);
    }
}



