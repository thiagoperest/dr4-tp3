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

            // Relacionamento 1:N
            builder.HasOne(p => p.City)
                .WithMany(c => c.Properties)
                .HasForeignKey(p => p.CityId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}