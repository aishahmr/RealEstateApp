using System;
namespace RealEstateAPI.Models
{
    public class Image
    {
        public Guid Id { get; set; }  // Primary Key
        public Guid PropertyId { get; set; }  // Foreign Key (Property)
        public string ImageUrl { get; set; }

        // Navigation Property
        public Property Property { get; set; }
    }
}
