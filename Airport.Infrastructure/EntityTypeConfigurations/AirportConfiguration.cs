using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Airport.Infrastructure.EntityTypeConfigurations;

using Airport = Domain.Entities.Airport;

public class AirportConfiguration : IEntityTypeConfiguration<Airport>
{
    public void Configure(EntityTypeBuilder<Airport> modelBuilder)
    {
        modelBuilder.HasKey(a => a.Id);

        modelBuilder
            .HasIndex(a => a.Iata)
            .IsUnique();
        
        modelBuilder
            .HasIndex(a => a.Icao)
            .IsUnique();
            
        modelBuilder
            .Property(a => a.Iata)
            .IsRequired()
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
        
        modelBuilder
            .HasOne(a => a.Location)
            .WithOne(c => c.Airport)
            .OnDelete(DeleteBehavior.Cascade);
    }
}