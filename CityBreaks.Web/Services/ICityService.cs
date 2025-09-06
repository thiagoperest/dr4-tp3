using CityBreaks.Web.Model;

namespace CityBreaks.Web.Services
{
    public interface ICityService
    {
        Task<List<City>> GetAllAsync();
    }
}