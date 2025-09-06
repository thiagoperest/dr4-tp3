using CityBreaks.Web.Model;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CityBreaks.Web.Data.Configurations
{
    public class CountryConfiguration : IEntityTypeConfiguration<Country>
    {
        public void Configure(EntityTypeBuilder<Country> builder)
        {
            builder.HasKey(e => e.Id);
            
            builder.Property(e => e.CountryCode)
                .IsRequired()
                .HasMaxLength(10)
                .HasColumnName("CountryCode");
            
            builder.Property(e => e.CountryName)
                .IsRequired()
                .HasMaxLength(100)
                .HasColumnName("CountryName");

            // Seed Data
            builder.HasData(
                new Country { Id = 1, CountryCode = "BR", CountryName = "Brasil" },
                new Country { Id = 2, CountryCode = "PT", CountryName = "Portugal" },
                new Country { Id = 3, CountryCode = "FR", CountryName = "França" },
                new Country { Id = 4, CountryCode = "IT", CountryName = "Itália" },
                new Country { Id = 5, CountryCode = "ES", CountryName = "Espanha" }
            );
        }
    }
}