using Airport.Application.Interfaces;
using Airport.Domain.Entities;
using Airport.Infrastructure.AppDbContext;
using Microsoft.EntityFrameworkCore;

namespace Airport.Infrastructure.Repositories;

public class CityRepository : ICityRepository
{
    private readonly AirportDbContext _airportDbContext;

    public CityRepository(AirportDbContext airportDbContext)
    {
        _airportDbContext = airportDbContext;
    }
    
    public Task<IEnumerable<City>> GetAllAsync(CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }

    public Task<City?> GetItemByIdAsync(Guid id, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }

    public Task<Guid> CreateAsync(City item, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }

    public Task UpdateAsync(City item, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }

    public Task<bool> DeleteAsync(City item, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }

    public async Task<City?> GetByCityNameAsync(string cityName, CancellationToken cancellationToken)
    {
        return await _airportDbContext.Cities
            .Include(c => c.Country)
            .FirstOrDefaultAsync(c => c.Name.ToLower() == cityName.ToLower(), cancellationToken);
    }
}