using CityBreaks.Web.Model;
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
            modelBuilder.Entity<Country>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.Property(e => e.CountryCode).IsRequired().HasMaxLength(10);
                entity.Property(e => e.CountryName).IsRequired().HasMaxLength(100);
            });

            modelBuilder.Entity<City>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.Property(e => e.Name).IsRequired().HasMaxLength(100);
                entity.Property(e => e.CountryId).IsRequired();

                // Relacionamento 1:N
                entity.HasOne(c => c.Country)
                      .WithMany(co => co.Cities)
                      .HasForeignKey(c => c.CountryId)
                      .OnDelete(DeleteBehavior.Cascade);
            });

            modelBuilder.Entity<Property>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.Property(e => e.Name).IsRequired().HasMaxLength(200);
                entity.Property(e => e.PricePerNight).IsRequired().HasColumnType("decimal(18,2)");
                entity.Property(e => e.CityId).IsRequired();

                // Relacionamento 1:N
                entity.HasOne(p => p.City)
                      .WithMany(c => c.Properties)
                      .HasForeignKey(p => p.CityId)
                      .OnDelete(DeleteBehavior.Cascade);
            });
        }
    }
}