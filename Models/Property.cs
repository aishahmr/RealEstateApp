using System;
using System.Collections.Generic;

namespace RealEstateAPI.Models
{
    public class Property
    {
        public Guid Id { get; set; }
        public string UserId { get; set; }
        public decimal Price { get; set; }
        public string Location { get; set; }
        public int Size { get; set; }
        public int Bedrooms { get; set; }
        public int Bathrooms { get; set; }
        public bool Kitchen { get; set; }
        public string Type { get; set; }
        public string Description { get; set; }
        public string VerificationStatus { get; set; }
        public string? DocumentUrl { get; set; }
        public bool IsFeatured { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public string FurnishingStatus { get; set; }
        public string Amenities { get; set; }

       
        public ApplicationUser User { get; set; }
        public ICollection<Image> Images { get; set; }
        public ICollection<PriceEstimate> PriceEstimates { get; set; }
        public ICollection<Booking> Bookings { get; set; } 
    }
}
