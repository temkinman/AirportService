using Airport.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Airport.Infrastructure.EntityTypeConfigurations;

public class AirportConfiguration : IEntityTypeConfiguration<Domain.Entities.Airport>
{
    public void Configure(EntityTypeBuilder<Domain.Entities.Airport> modelBuilder)
    {
        modelBuilder.HasKey(a => a.Id);

        modelBuilder
            .Property(a => a.Iata)
            .HasMaxLength(3);

        modelBuilder
            .Property(a => a.Icao)
            .HasMaxLength(4);

        modelBuilder
            .Property(a => a.Name)
            .IsRequired()
            .HasMaxLength(100);

        modelBuilder
            .HasOne(a => a.City)
            .WithMany(c => c.Airports)
            .HasForeignKey(a => a.CityId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}