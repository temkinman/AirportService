using Airport.Domain.Entities;

namespace Airport.Application.Interfaces;

public interface ILocationRepository : IBaseItemRepository<Location>
{
    Task<Location?> GetByCoordinatesAsync(double lon, double lat, CancellationToken cancellationToken = default);
}