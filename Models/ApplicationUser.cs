using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations; 
namespace RealEstateAPI.Models
{
    public class ApplicationUser : IdentityUser
    {
        public ApplicationUser()
        {
            Properties = new List<Property>();
            Favorites = new List<Favorite>();
            Filters = new List<Filter>();
            Bookings = new List<Booking>();
        }

        [Required] 
        public override string Email { get; set; } = string.Empty;

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public bool IsActive { get; set; } = true;

        public ICollection<Property> Properties { get; set; }
        public ICollection<Favorite> Favorites { get; set; }
        public ICollection<Filter> Filters { get; set; }
        public ICollection<Booking> Bookings { get; set; }
    }
}
