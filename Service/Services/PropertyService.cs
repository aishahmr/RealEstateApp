using RealEstateAPI.Models;
using RealEstateAPI.Service.IServices;
using RealEstateAPI.Models.Data;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace RealEstateAPI.Service.Services
{
    public class PropertyService : IPropertyService
    {
        private readonly AppDbContext _context;

        public PropertyService(AppDbContext context)
        {
            _context = context;
        }

        public async Task<List<Property>> GetAllPropertiesAsync()
        {
            return await _context.Properties.ToListAsync();
        }

        public async Task<Property> GetPropertyByIdAsync(int id)
        {
            return await _context.Properties.FindAsync(id);
        }

        public async Task<bool> AddPropertyAsync(Property property)
        {
            _context.Properties.Add(property);
            return await _context.SaveChangesAsync() > 0;
        }

        public async Task<bool> UpdatePropertyAsync(Property property)
        {
            _context.Properties.Update(property);
            return await _context.SaveChangesAsync() > 0;
        }

        public async Task<bool> DeletePropertyAsync(int id)
        {
            var property = await _context.Properties.FindAsync(id);
            if (property == null) return false;

            _context.Properties.Remove(property);
            return await _context.SaveChangesAsync() > 0;
        }
    }
}
