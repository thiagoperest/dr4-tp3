using CityBreaks.Web.Model;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace CityBreaks.Web.Services
{
    public interface ICityService
    {
        Task<List<City>> GetAllAsync();
        Task<City?> GetByNameAsync(string name);
        Task<SelectList> GetCitiesSelectListAsync();
    }
}