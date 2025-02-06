namespace Airport.Application.Interfaces;

public interface IBaseItemRepository<T>
{
    Task<IEnumerable<T>> GetAllAsync(CancellationToken cancellationToken = default);
    Task<T?> GetItemByIdAsync(Guid id, CancellationToken cancellationToken = default);
    Task<Guid> CreateAsync(T item, CancellationToken cancellationToken = default);
    Task UpdateAsync(T item, CancellationToken cancellationToken = default);
    Task<bool> DeleteAsync(T item, CancellationToken cancellationToken = default);
}