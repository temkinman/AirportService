using System.Text.Json;
using Airport.Application.Helpers;
using Airport.Application.Interfaces;
using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Airport.Application.Airports.Queries;

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

    public GetAirportInfoByIataHandler(HttpClient httpClient, IAirportRepository airportRepository, IMapper mapper,
        ICityRepository cityRepository, ICountryRepository countryRepository, ILocationRepository locationRepository,
        ILogger<GetAirportInfoByIataHandler> logger)
    {
        _airportRepository = airportRepository;
        _mapper = mapper;
        _cityRepository = cityRepository;
        _countryRepository = countryRepository;
        _locationRepository = locationRepository;
        _logger = logger;
        _httpClient = HttpClientFactory.CreateClient();
    }
    
    public async Task<GetAirportInfoByIataResult?> Handle(GetAirportInfoByIataQuery query, CancellationToken cancellationToken)
    {
        var airportExisting = await _airportRepository.GetAirportDataByIataAsync(query.Iata);
        
        if (airportExisting != null)
        {
            _logger.LogInformation("Getting airport data from database");
            var res = _mapper.Map<GetAirportInfoByIataResult>(airportExisting);
            _mapper.ConfigurationProvider.AssertConfigurationIsValid();
            return res;
        }
        
        GetAirportInfoByIataResult? result;
        using (var request = new HttpRequestMessage(HttpMethod.Get, $"{_httpClient.BaseAddress}/airports/{query.Iata}"))
        {
            HttpResponseMessage response = await _httpClient.SendAsync(request, cancellationToken);

            if (response.IsSuccessStatusCode)
            {
                _logger.LogInformation("Getting airport data from remote api");
                
                var jsonResponse = await response.Content.ReadAsStringAsync(cancellationToken);
                
                // тут приходится options сетать, почему-то конфигурация из program не работает
                result = JsonSerializer.Deserialize<GetAirportInfoByIataResult>(jsonResponse, new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true
                });
                
                var airport = _mapper.Map<Airport>(result);
                
                airport = await SetAirportEntitiesAsync(airport, cancellationToken);
                
                _logger.LogInformation("Creating new airport");
                await _airportRepository.CreateAsync(airport, cancellationToken);
            }
            else
            {
                var errorMessage = $"Error: {response.StatusCode} - {response.ReasonPhrase}";
                throw new HttpRequestException(errorMessage);
            }
        }

        return result;
    }
    private async Task<Airport> SetAirportEntitiesAsync(Airport airport, CancellationToken cancellationToken)
    {
        airport = await AirportHelper.SetCountryToAirportAsync(_countryRepository, airport, cancellationToken);
        airport = await AirportHelper.SetCityToAirportAsync(_cityRepository, _countryRepository, airport, cancellationToken);
        airport = await AirportHelper.SetLocationToAirportAsync(_locationRepository, airport, cancellationToken);

        return airport;
    }

    
}