namespace RealEstateAPI.DTOs.FavoriteDTO
{
    public class FavoriteDTO
    {
        public Guid Id { get; set; }
        public string UserId { get; set; }
        public Guid PropertyId { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}
