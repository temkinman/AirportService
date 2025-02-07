using Airport.Domain.Entities;

namespace Airport.Application.Interfaces;

public interface ICityRepository : IBaseItemRepository<City>
{
    Task<City?> GetByCityNameAsync(string cityName, CancellationToken cancellationToken = default);
}