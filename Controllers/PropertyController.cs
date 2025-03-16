using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RealEstateAPI.Models;
using RealEstateAPI.Models.Data;

namespace RealEstateAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PropertyController : ControllerBase
    {
        private readonly AppDbContext _context; 

        public PropertyController(AppDbContext context)
        {
            _context = context;
        }

        [HttpGet("GetAllProperties")]
        public async Task<IActionResult> GetAllProperties(
            string? location,
            decimal? minPrice,
            decimal? maxPrice,
            int? minSize,
            int? maxSize,
            int? bedrooms,
            int? bathrooms,
            string? type,
            string? availability,
            string? furnishingStatus,
            [FromQuery] List<string>? amenities)
        {
            var properties = _context.Properties.AsQueryable();

            if (!string.IsNullOrEmpty(location))
                properties = properties.Where(p => p.Location.Contains(location));

            if (minPrice.HasValue)
                properties = properties.Where(p => p.Price >= minPrice.Value);

            if (maxPrice.HasValue)
                properties = properties.Where(p => p.Price <= maxPrice.Value);

            if (minSize.HasValue)
                properties = properties.Where(p => p.Size >= minSize.Value);

            if (maxSize.HasValue)
                properties = properties.Where(p => p.Size <= maxSize.Value);

            if (bedrooms.HasValue)
                properties = properties.Where(p => p.Bedrooms == bedrooms.Value);

            if (bathrooms.HasValue)
                properties = properties.Where(p => p.Bathrooms == bathrooms.Value);

            if (!string.IsNullOrEmpty(type))
                properties = properties.Where(p => p.Type == type);

            if (!string.IsNullOrEmpty(availability))
                properties = properties.Where(p => p.VerificationStatus == availability);

            if (!string.IsNullOrEmpty(furnishingStatus))
                properties = properties.Where(p => p.FurnishingStatus == furnishingStatus);

            if (amenities != null && amenities.Count > 0)
                properties = properties.Where(p => amenities.All(a => p.Amenities.Contains(a)));

            return Ok(await properties.ToListAsync());
        }
    }
}
