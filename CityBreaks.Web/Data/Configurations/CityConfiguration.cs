using CityBreaks.Web.Model;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CityBreaks.Web.Data.Configurations
{
    public class CityConfiguration : IEntityTypeConfiguration<City>
    {
        public void Configure(EntityTypeBuilder<City> builder)
        {
            builder.HasKey(e => e.Id);
            
            builder.Property(e => e.Name)
                .IsRequired()
                .HasMaxLength(150)
                .HasColumnName("Name");
            
            builder.Property(e => e.CountryId)
                .IsRequired()
                .HasColumnName("CountryId");

            // Relacionamento 1:N
            builder.HasOne(c => c.Country)
                .WithMany(co => co.Cities)
                .HasForeignKey(c => c.CountryId)
                .OnDelete(DeleteBehavior.Cascade);

            // Seed Data
            builder.HasData(
                // Cidades do Brasil
                new City { Id = 1, Name = "São Paulo", CountryId = 1 },
                new City { Id = 2, Name = "Rio de Janeiro", CountryId = 1 },
                new City { Id = 3, Name = "Salvador", CountryId = 1 },
                
                // Cidades de Portugal
                new City { Id = 4, Name = "Lisboa", CountryId = 2 },
                new City { Id = 5, Name = "Porto", CountryId = 2 },
                
                // Cidades da França
                new City { Id = 6, Name = "Paris", CountryId = 3 },
                new City { Id = 7, Name = "Nice", CountryId = 3 },
                
                // Cidades da Itália
                new City { Id = 8, Name = "Roma", CountryId = 4 },
                new City { Id = 9, Name = "Veneza", CountryId = 4 },
                
                // Cidades da Espanha
                new City { Id = 10, Name = "Madrid", CountryId = 5 },
                new City { Id = 11, Name = "Barcelona", CountryId = 5 }
            );
        }
    }
}