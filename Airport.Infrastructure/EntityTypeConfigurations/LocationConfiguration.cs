using Airport.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Airport.Infrastructure.EntityTypeConfigurations;

public class LocationConfiguration : IEntityTypeConfiguration<Location>
{
    public void Configure(EntityTypeBuilder<Location> modelBuilder)
    {
        modelBuilder
            .HasKey(l => l.Id);
        
        modelBuilder
            .HasIndex(a => new { a.Lat, a.Lon })
            .IsUnique();

        modelBuilder
            .Property(l => l.Lon)
            .IsRequired();

        modelBuilder
            .Property(l => l.Lat)
            .IsRequired();
    }
}