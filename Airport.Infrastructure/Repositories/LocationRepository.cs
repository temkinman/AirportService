using Airport.Application.Interfaces;
using Airport.Domain.Entities;
using Airport.Infrastructure.AppDbContext;
using Microsoft.EntityFrameworkCore;

namespace Airport.Infrastructure.Repositories;

public class LocationRepository : ILocationRepository
{
    private readonly AirportDbContext _airportDbContext;

    public LocationRepository(AirportDbContext airportDbContext)
    {
        _airportDbContext = airportDbContext;
    }
    
    public Task<IEnumerable<Location>> GetAllAsync(CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }

    public Task<Location?> GetItemByIdAsync(Guid id, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }

    public Task<Guid> CreateAsync(Location item, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }

    public Task UpdateAsync(Location item, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }

    public Task<bool> DeleteAsync(Location item, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }

    public async Task<Location?> GetByCoordinatesAsync(double lon, double lat, CancellationToken cancellationToken)
    {
        double diff = 0.0000001f;
        return await _airportDbContext.Locations
            .AsNoTracking()
            .FirstOrDefaultAsync(l => Math.Abs(l.Lon - lon) < diff && Math.Abs(l.Lat - lat) < diff, cancellationToken); 
    }
}