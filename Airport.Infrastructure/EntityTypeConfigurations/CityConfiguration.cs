using Airport.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Airport.Infrastructure.EntityTypeConfigurations;

public class CityConfiguration : IEntityTypeConfiguration<City>
{
    public void Configure(EntityTypeBuilder<City> modelBuilder)
    {
        modelBuilder.HasKey(c => c.Id);

        modelBuilder
            .Property(c => c.Name)
            .IsRequired()
            .HasMaxLength(100);

        modelBuilder
            .Property(c => c.Iata)
            .HasMaxLength(3);

        modelBuilder
            .HasOne(c => c.Country)
            .WithMany(c => c.Cities)
            .HasForeignKey(c => c.CountryId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}