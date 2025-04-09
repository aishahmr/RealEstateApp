﻿namespace RealEstateAPI.DTOs.UserDTos.Profile
{
    public class UserDetailsDTO
    {
        public string UserName { get; set; }
        public string Email { get; set; }
        public string? PhoneNumber { get; set; }
        public string? Bio { get; set; }
        public string? Gender { get; set; }
        public string? ProfilePictureUrl { get; set; }
    }
}

