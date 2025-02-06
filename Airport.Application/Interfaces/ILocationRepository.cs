using Airport.Domain.Entities;

namespace Airport.Application.Interfaces;

public class ILocationRepository : IBaseItemRepository<Location>
{
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
}