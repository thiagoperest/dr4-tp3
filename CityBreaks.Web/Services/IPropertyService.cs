using CityBreaks.Web.Model;

namespace CityBreaks.Web.Services
{
    public interface IPropertyService
    {
        Task<bool> CreateAsync(Property property);
    }
}