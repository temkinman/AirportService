using Airport.Domain.Entities;

namespace Airport.Application.Interfaces;

public interface ICountryRepository : IBaseItemRepository<Country>
{
    Task<Country?> GetByCountryNameAsync(string countryName, CancellationToken cancellationToken = default);
}