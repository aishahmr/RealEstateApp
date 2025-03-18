﻿namespace RealEstateAPI.DTOs.UserDTos.UserManagement
{
    public class UserResponseDTO
    {

        public string Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string Role { get; set; }
        public string? ImageUrl { get; set; }

    }
}
