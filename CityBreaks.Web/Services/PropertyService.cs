using CityBreaks.Web.Data;
using CityBreaks.Web.Model;

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
    }
}