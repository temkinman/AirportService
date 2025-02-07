using Airport.Application.Interfaces;
using Airport.Domain.Entities;
using Airport.Infrastructure.AppDbContext;
using Microsoft.EntityFrameworkCore;

namespace Airport.Infrastructure.Repositories;

public class CountryRepository : ICountryRepository
{
    private readonly AirportDbContext _airportDbContext;

    public CountryRepository(AirportDbContext airportDbContext)
    {
        _airportDbContext = airportDbContext;
    }
    
    public Task<IEnumerable<Country>> GetAllAsync(CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }

    public Task<Country?> GetItemByIdAsync(Guid id, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }

    public async Task<Guid> CreateAsync(Country country, CancellationToken cancellationToken)
    {
        _airportDbContext.Countries.Add(country);
        await _airportDbContext.SaveChangesAsync(cancellationToken);
        
        return country.Id;
    }

    public Task UpdateAsync(Country item, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }

    public Task<bool> DeleteAsync(Country item, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }

    public async Task<Country?> GetByCountryNameAsync(string countryName, CancellationToken cancellationToken)
    {
        return await _airportDbContext.Countries
            .AsNoTracking()
            .FirstOrDefaultAsync(c => c.Name.ToLower() == countryName.ToLower(), cancellationToken);
    }
}