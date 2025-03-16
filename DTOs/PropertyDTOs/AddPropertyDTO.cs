namespace RealEstateAPI.DTOs.PropertyDTOs
{
    public class AddPropertyDTO
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
        public string Location { get; set; }
        public int Bedrooms { get; set; }
        public int Bathrooms { get; set; }
        public double Area { get; set; }
        public List<string>? ImageUrls { get; set; }
    }
}
