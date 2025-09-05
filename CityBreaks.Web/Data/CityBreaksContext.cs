using CityBreaks.Web.Model;
using CityBreaks.Web.Data.Configurations;
using Microsoft.EntityFrameworkCore;

namespace CityBreaks.Web.Data
{
    public class CityBreaksContext : DbContext
    {
        public CityBreaksContext(DbContextOptions<CityBreaksContext> options)
            : base(options)
        {
        }

        public DbSet<Country> Countries { get; set; }
        public DbSet<City> Cities { get; set; }
        public DbSet<Property> Properties { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Fluent API
            modelBuilder.ApplyConfiguration(new CountryConfiguration());
            modelBuilder.ApplyConfiguration(new CityConfiguration());
            modelBuilder.ApplyConfiguration(new PropertyConfiguration());
        }
    }
}