using Airport.Domain.Entities;

namespace Airport.Application.Interfaces;

public class ICityRepository : IBaseItemRepository<City>
{
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
}