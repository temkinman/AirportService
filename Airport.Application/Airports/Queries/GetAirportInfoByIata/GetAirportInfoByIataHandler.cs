using System.Text.Json;
using Airport.Application.Helpers;
using Airport.Application.Interfaces;
using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Airport.Application.Airports.Queries.GetAirportInfoByIata;

using Airport = Domain.Entities.Airport;
public class GetAirportInfoByIataHandler : IRequestHandler<GetAirportInfoByIataQuery, GetAirportInfoByIataResult?>
{
    private readonly HttpClient _httpClient;
    private readonly IAirportRepository _airportRepository;
    private readonly ICityRepository _cityRepository;
    private readonly ICountryRepository _countryRepository;
    private readonly ILocationRepository _locationRepository;
    private readonly IMapper _mapper;
    private ILogger<GetAirportInfoByIataHandler> _logger;
    private readonly JsonSerializerOptions _jsonOptions;

    public GetAirportInfoByIataHandler(HttpClient httpClient, IAirportRepository airportRepository, IMapper mapper,
        ICityRepository cityRepository, ICountryRepository countryRepository, ILocationRepository locationRepository,
        ILogger<GetAirportInfoByIataHandler> logger, JsonSerializerOptions jsonOptions)
    {
        _airportRepository = airportRepository;
        _mapper = mapper;
        _cityRepository = cityRepository;
        _countryRepository = countryRepository;
        _locationRepository = locationRepository;
        _logger = logger;
        _jsonOptions = jsonOptions;
        _httpClient = HttpClientFactory.CreateClient();
    }
    
    public async Task<GetAirportInfoByIataResult?> Handle(GetAirportInfoByIataQuery query, CancellationToken cancellationToken)
    {
        var airportExisting = await _airportRepository.GetAirportDataByIataAsync(query.Iata, cancellationToken);
        
        if (airportExisting != null)
        {
            _logger.LogInformation("******** Getting airport data from database ************");
            var res = _mapper.Map<GetAirportInfoByIataResult>(airportExisting);
            return res;
        }
        
        (GetAirportInfoByIataResult? result, Airport? airport) = await AirportHelper.GetAirportDataFromApiAsync(_httpClient, query.Iata, _mapper,
            _jsonOptions, cancellationToken);

        if (airport != null)
        {
            _logger.LogInformation("********* Creating new airport *****************");
            airport = await AirportHelper.SetAirportEntitiesAsync(airport,
                _countryRepository, _cityRepository, _locationRepository, cancellationToken);
            
            await _airportRepository.CreateAsync(airport, cancellationToken);    
        }

        return result;
    }
}