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
        }
    }
}