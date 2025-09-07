using CityBreaks.Web.Model;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CityBreaks.Web.Data.Configurations
{
    public class PropertyConfiguration : IEntityTypeConfiguration<Property>
    {
        public void Configure(EntityTypeBuilder<Property> builder)
        {
            builder.HasKey(e => e.Id);
            
            builder.Property(e => e.Name)
                .IsRequired()
                .HasMaxLength(250)
                .HasColumnName("Name");
            
            builder.Property(e => e.PricePerNight)
                .IsRequired()
                .HasColumnType("decimal(18,2)")
                .HasColumnName("PricePerNight");
            
            builder.Property(e => e.CityId)
                .IsRequired()
                .HasColumnName("CityId");

            builder.Property(e => e.DeletedAt)
                   .HasColumnName("DeletedAt");

            builder.HasQueryFilter(p => p.DeletedAt == null);

            // Relacionamento 1:N
            builder.HasOne(p => p.City)
                .WithMany(c => c.Properties)
                .HasForeignKey(p => p.CityId)
                .OnDelete(DeleteBehavior.Cascade);

            // Seed Data
            builder.HasData(
                // Propriedades em São Paulo
                new Property { Id = 1, Name = "Hotel Copacabana Palace", PricePerNight = 450.00m, CityId = 1 },
                new Property { Id = 2, Name = "Apartamento Centro SP", PricePerNight = 120.00m, CityId = 1 },
                
                // Propriedades no Rio de Janeiro
                new Property { Id = 3, Name = "Hotel Ipanema Beach", PricePerNight = 380.00m, CityId = 2 },
                new Property { Id = 4, Name = "Pousada Santa Teresa", PricePerNight = 180.00m, CityId = 2 },
                
                // Propriedades em Lisboa
                new Property { Id = 5, Name = "Hotel Dom Pedro Palace", PricePerNight = 200.00m, CityId = 4 },
                new Property { Id = 6, Name = "Apartamento Chiado", PricePerNight = 90.00m, CityId = 4 },
                
                // Propriedades em Paris
                new Property { Id = 7, Name = "Hotel Ritz Paris", PricePerNight = 800.00m, CityId = 6 },
                new Property { Id = 8, Name = "Apartamento Montmartre", PricePerNight = 150.00m, CityId = 6 },
                
                // Propriedades em Rom
                new Property { Id = 9, Name = "Hotel Colosseum View", PricePerNight = 320.00m, CityId = 8 },
                new Property { Id = 10, Name = "B&B Vatican", PricePerNight = 110.00m, CityId = 8 },
                
                // Propriedades em Barcelona
                new Property { Id = 11, Name = "Hotel Sagrada Familia", PricePerNight = 280.00m, CityId = 11 },
                new Property { Id = 12, Name = "Apartamento Gothic Quarter", PricePerNight = 95.00m, CityId = 11 }
            );
        }
    }
}