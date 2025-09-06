using CityBreaks.Web.Data;
using CityBreaks.Web.Model;
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
    }
}