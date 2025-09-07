using CityBreaks.Web.Data;
using CityBreaks.Web.Model;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace CityBreaks.Web.Services
{
    public class CityService : ICityService
    {
        private readonly CityBreaksContext _context;

        public CityService(CityBreaksContext context)
        {
            _context = context;
        }

        public async Task<List<City>> GetAllAsync()
        {
            return await _context.Cities
                .Include(c => c.Country)
                .Include(c => c.Properties)
                .ToListAsync();
        }

        public async Task<City?> GetByNameAsync(string name)
        {
            return await _context.Cities
                .Include(c => c.Country)
                .Include(c => c.Properties)
                .FirstOrDefaultAsync(c => EF.Functions.Collate(c.Name, "NOCASE") == name);
        }

        public async Task<SelectList> GetCitiesSelectListAsync()
        {
            var cities = await _context.Cities
                .Include(c => c.Country)
                .OrderBy(c => c.Country.CountryName)
                .ThenBy(c => c.Name)
                .Select(c => new
                {
                    Value = c.Id,
                    Text = $"{c.Name} - {c.Country.CountryName}"
                })
                .ToListAsync();

            return new SelectList(cities, "Value", "Text");
        }
    }
}