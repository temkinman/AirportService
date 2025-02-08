using Airport.Application.Interfaces;
using Airport.Infrastructure.AppDbContext;
using Microsoft.EntityFrameworkCore;

namespace Airport.Infrastructure.Repositories;

using Airport = Domain.Entities.Airport;

public class AirportRepository : IAirportRepository
{
    private readonly AirportDbContext _airportDbContext;

    public AirportRepository(AirportDbContext airportDbContext)
    {
        _airportDbContext = airportDbContext;
    }
    
    public Task<IEnumerable<Airport>> GetAllAsync(CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }

    public Task<Airport?> GetItemByIdAsync(Guid id, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }

    public async Task<Guid> CreateAsync(Airport airport, CancellationToken cancellationToken)
    {
        _airportDbContext.Airports.Add(airport);
        await _airportDbContext.SaveChangesAsync(cancellationToken);
        
        return airport.Id;
    }

    public Task UpdateAsync(Airport item, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }

    public Task<bool> DeleteAsync(Airport item, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }

    public async Task<Airport?> GetAirportDataByIataAsync(string iata, CancellationToken cancellationToken)
    {
        return await _airportDbContext.Airports
            .Include(a => a.City)
            .Include(a => a.Country)
            .Include(a => a.Location)
            .FirstOrDefaultAsync(a => a.Iata.ToLower() == iata.ToLower(), cancellationToken);
    }
}