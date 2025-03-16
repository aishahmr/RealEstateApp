using RealEstateAPI.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace RealEstateAPI.Service.IServices
{
    public interface IPropertyService
    {
        Task<List<Property>> GetAllPropertiesAsync();
        Task<Property> GetPropertyByIdAsync(int id);
        Task<bool> AddPropertyAsync(Property property);
        Task<bool> UpdatePropertyAsync(Property property);
        Task<bool> DeletePropertyAsync(int id);
    }
}
