namespace RealEstateAPI.DTOs.PropertyDTOs
{
    public class PropertyDTO
    {
        public Guid Id { get; set; }
        public decimal Price { get; set; }
        public string Location { get; set; }
        public int Size { get; set; }
        public int Bedrooms { get; set; }
        public int Bathrooms { get; set; }
        public bool Kitchen { get; set; }
        public string Type { get; set; }  // apartment, studio, villa
        public string Description { get; set; }
        public string VerificationStatus { get; set; } // pending, verified, rejected
        public string DocumentUrl { get; set; }
        public bool IsFeatured { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}
