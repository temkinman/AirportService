using Airport.Domain.Entities;

namespace Airport.Application.Interfaces;

public class ICountryRepository : IBaseItemRepository<Country>
{
    public Task<IEnumerable<Country>> GetAllAsync(CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }

    public Task<Country?> GetItemByIdAsync(Guid id, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }

    public Task<Guid> CreateAsync(Country item, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }

    public Task UpdateAsync(Country item, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }

    public Task<bool> DeleteAsync(Country item, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}