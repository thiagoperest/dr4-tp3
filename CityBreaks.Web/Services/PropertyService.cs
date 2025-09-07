using CityBreaks.Web.Data;
using CityBreaks.Web.Model;
using Microsoft.EntityFrameworkCore;

namespace CityBreaks.Web.Services
{
    public class PropertyService : IPropertyService
    {
        private readonly CityBreaksContext _context;

        public PropertyService(CityBreaksContext context)
        {
            _context = context;
        }

        public async Task<bool> CreateAsync(Property property)
        {
            try
            {
                await _context.Properties.AddAsync(property);
                await _context.SaveChangesAsync();
                return true;
            }
            catch
            {
                return false;
            }
        }

        public async Task<Property?> GetByIdAsync(int id)
        {
            return await _context.Properties
                .Include(p => p.City)
                .ThenInclude(c => c.Country)
                .FirstOrDefaultAsync(p => p.Id == id);
        }

        public async Task<bool> UpdateAsync(int id, string name, decimal pricePerNight, int cityId)
        {
            try
            {
                var property = await _context.Properties.FindAsync(id);
                if (property == null)
                    return false;

                property.Name = name;
                property.PricePerNight = pricePerNight;
                property.CityId = cityId;

                await _context.SaveChangesAsync();
                return true;
            }
            catch
            {
                return false;
            }
        }

        public async Task<bool> DeleteAsync(int id)
        {
            try
            {
                var property = await _context.Properties.FindAsync(id);
                if (property == null)
                    return false;

                property.DeletedAt = DateTime.UtcNow;
                await _context.SaveChangesAsync();
                return true;
            }
            catch
            {
                return false;
            }
        }

        public async Task<List<Property>> GetAllAsync()
        {
            return await _context.Properties
                .Include(p => p.City)
                .ThenInclude(c => c.Country)
                .ToListAsync();
        }

        public async Task<List<Property>> GetDeletedAsync()
        {
            return await _context.Properties
                .IgnoreQueryFilters()
                .Include(p => p.City)
                .ThenInclude(c => c.Country)
                .Where(p => p.DeletedAt != null)
                .ToListAsync();
        }

        public async Task<List<Property>> GetFilteredAsync(decimal? minPrice, decimal? maxPrice, string? cityName, string? propertyName)
        {
            var query = _context.Properties
                .Include(p => p.City)
                .ThenInclude(c => c.Country)
                .AsQueryable();

            if (minPrice.HasValue)
            {
                query = query.Where(p => p.PricePerNight >= minPrice.Value);
            }

            if (maxPrice.HasValue)
            {
                query = query.Where(p => p.PricePerNight <= maxPrice.Value);
            }

            if (!string.IsNullOrWhiteSpace(cityName))
            {
                query = query.Where(p => EF.Functions.Collate(p.City.Name, "NOCASE").Contains(cityName));
            }
            
            if (!string.IsNullOrWhiteSpace(propertyName))
            {
                query = query.Where(p => EF.Functions.Collate(p.Name, "NOCASE").Contains(propertyName));
            }

            var results = await query.ToListAsync();

            return results.OrderBy(p => p.PricePerNight).ToList();
        }
    }
}