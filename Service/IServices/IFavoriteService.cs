using RealEstateAPI.Models;
using System.Collections.Generic;
using System.Threading.Tasks;
using System;

namespace RealEstateAPI.Service.IServices
{
    public interface IFavoriteService
    {
        Task<List<Favorite>> GetUserFavorites(string userId);
        Task<bool> AddToFavorites(string userId, Guid propertyId);
        Task<bool> RemoveFromFavorites(string userId, Guid propertyId);
    }
}


