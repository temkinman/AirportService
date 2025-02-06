namespace Airport.Application.Interfaces;

using Airport = Domain.Entities.Airport;

public class IAirportRepository : IBaseItemRepository<Airport>
{
    public Task<IEnumerable<Airport>> GetAllAsync(CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }

    public Task<Airport?> GetItemByIdAsync(Guid id, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }

    public Task<Guid> CreateAsync(Airport item, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }

    public Task UpdateAsync(Airport item, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }

    public Task<bool> DeleteAsync(Airport item, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}