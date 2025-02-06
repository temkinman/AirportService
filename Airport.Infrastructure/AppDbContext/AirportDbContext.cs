using System.Reflection;
using Airport.Application.Interfaces;
using Airport.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Airport.Infrastructure.AppDbContext;

using Airport = Domain.Entities.Airport;

public class AirportDbContext(DbContextOptions<AirportDbContext> options) : DbContext(options), IAirportDbContext
{
    public DbSet<Country> Countries { get; set; }
    public DbSet<City> Cities { get; set; }
    public DbSet<Airport> Airports { get; set; }
    public DbSet<Location> Locations { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
        base.OnModelCreating(modelBuilder);
    }
}