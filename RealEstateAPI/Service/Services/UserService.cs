﻿using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using RealEstateAPI.DTOs.UserDTos.Profile;
using RealEstateAPI.DTOs.UserDTos.UserManagement;
using RealEstateAPI.Models;
using RealEstateAPI.Service.IServices;
using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;

namespace RealEstateAPI.Service.Services
{
    public class UserService : IUserService
    {
        private readonly UserManager<ApplicationUser> _userManager;

        public UserService(UserManager<ApplicationUser> userManager)
        {
            _userManager = userManager;
        }

        public async Task<ApplicationUser> GetUserByIdAsync(string userId)
        {
            return await _userManager.Users
                .FirstOrDefaultAsync(u =>
                    EF.Functions.Collate(u.Id, "SQL_Latin1_General_CP1_CS_AS") == userId);
        }

        public async Task<UserProfileDTO> GetUserDetails(string userId)
        {
            var user = await _userManager.Users
                .FirstOrDefaultAsync(u =>
                    EF.Functions.Collate(u.Id, "SQL_Latin1_General_CP1_CS_AS") == userId);

            if (user == null) return null;

            return new UserProfileDTO
            {
                Id = user.Id,
                Email = user.Email,
                PhoneNumber = user.PhoneNumber,
                Bio = user.Bio,
                Gender = user.Gender,
                ProfilePictureUrl = user.ProfilePictureUrl
            };
        }

        public async Task<UserProfileDTO> GetLoggedInUserProfile(string userId)
        {
            var user = await _userManager.Users
                .FirstOrDefaultAsync(u =>
                    EF.Functions.Collate(u.Id, "SQL_Latin1_General_CP1_CS_AS") == userId);

            if (user == null) return null;

            return new UserProfileDTO
            {
                PhoneNumber = user.PhoneNumber,
                Bio = user.Bio,
                Gender = user.Gender,
                ProfilePictureUrl = user.ProfilePictureUrl
            };
        }

        public async Task<bool> UpdateUserProfile(string userId, UpdateUserProfileDTO dto, ILogger logger)
        {
            var user = await _userManager.Users
                .FirstOrDefaultAsync(u =>
                    EF.Functions.Collate(u.Id, "SQL_Latin1_General_CP1_CS_AS") == userId);

            if (user == null)
            {
                logger.LogError("❗ User not found");
                return false;
            }

            logger.LogInformation($"Updating profile: Phone={dto.PhoneNumber}, Bio={dto.Bio}, Gender={dto.Gender}");

            if (!string.IsNullOrWhiteSpace(dto.PhoneNumber))
            {
                var phoneResult = await _userManager.SetPhoneNumberAsync(user, dto.PhoneNumber);
                if (!phoneResult.Succeeded)
                {
                    foreach (var error in phoneResult.Errors)
                        logger.LogError($"❗ Phone error: {error.Code} - {error.Description}");
                    return false;
                }
            }

            user.Bio = dto.Bio ?? user.Bio;
            user.Gender = dto.Gender ?? user.Gender;

            var updateResult = await _userManager.UpdateAsync(user);
            if (!updateResult.Succeeded)
            {
                foreach (var error in updateResult.Errors)
                    logger.LogError($"❗ Update error: {error.Code} - {error.Description}");
                return false;
            }

            logger.LogInformation("✅ Profile updated successfully!");
            return true;
        }

        public async Task<string?> UploadProfilePicture(string userId, IFormFile file)
        {
            var user = await _userManager.Users
                .FirstOrDefaultAsync(u =>
                    EF.Functions.Collate(u.Id, "SQL_Latin1_General_CP1_CS_AS") == userId);

            if (user == null) return null;

            var uploadsDir = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/uploads");
            Directory.CreateDirectory(uploadsDir);

            var fileName = $"{Guid.NewGuid()}{Path.GetExtension(file.FileName)}";
            var filePath = Path.Combine(uploadsDir, fileName);

            using (var stream = new FileStream(filePath, FileMode.Create))
            {
                await file.CopyToAsync(stream);
            }

            user.ProfilePictureUrl = $"/uploads/{fileName}";
            await _userManager.UpdateAsync(user);

            return user.ProfilePictureUrl;
        }

        public async Task<bool> UpdateUserAsync(ApplicationUser user)
        {
            var result = await _userManager.UpdateAsync(user);
            return result.Succeeded;
        }

        // Methods to implement later
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

        public Task<bool> UnblockUser(string userId)
        {
            throw new NotImplementedException();
        }
    }
}