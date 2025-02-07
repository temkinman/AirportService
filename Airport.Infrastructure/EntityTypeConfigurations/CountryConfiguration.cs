using Airport.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Airport.Infrastructure.EntityTypeConfigurations;

public class CountryConfiguration : IEntityTypeConfiguration<Country>
{
    public void Configure(EntityTypeBuilder<Country> modelBuilder)
    {
        modelBuilder.HasKey(c => c.Id);
        
        modelBuilder
            .HasIndex(a => a.Iata)
            .IsUnique();
        
        modelBuilder
            .HasIndex(a => a.Name)
            .IsUnique();

        modelBuilder
            .Property(c => c.Name)
            .IsRequired()
            .HasMaxLength(100);

        modelBuilder
            .Property(c => c.Iata)
            .IsRequired()
            .HasMaxLength(3);
    }
}