using Airport.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Airport.Application.Interfaces;

using Airport = Domain.Entities.Airport;
public interface IAirportDbContext
{
    public DbSet<Country> Countries { get; set; }
    public DbSet<City> Cities { get; set; }
    public DbSet<Airport> Airports { get; set; }
    public DbSet<Location> Locations { get; set; }
    Task<int> SaveChangesAsync(CancellationToken token = default);
}