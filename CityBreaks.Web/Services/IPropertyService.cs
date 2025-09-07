using CityBreaks.Web.Model;

namespace CityBreaks.Web.Services
{
    public interface IPropertyService
    {
        Task<bool> CreateAsync(Property property);
        Task<Property?> GetByIdAsync(int id);
        Task<bool> UpdateAsync(int id, string name, decimal pricePerNight, int cityId);
    }
}