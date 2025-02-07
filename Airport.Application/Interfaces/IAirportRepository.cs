namespace Airport.Application.Interfaces;

using Airport = Domain.Entities.Airport;

public interface IAirportRepository : IBaseItemRepository<Airport>
{
    Task<Airport?> GetAirportDataByIataAsync(string iata, CancellationToken cancellationToken = default);
}