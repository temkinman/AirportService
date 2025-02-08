using System.Text.Json;
using Airport.Application.Airports.Queries.GetAirportInfoByIata;
using Airport.Application.Helpers;
using Airport.Application.Interfaces;
using Airport.Domain.Entities;
using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Airport.Application.Airports.Queries.GetDistanceBetweenTwoAirports;

using Airport = Domain.Entities.Airport;

public class GetDistanceBetweenTwoAirportsHandler : IRequestHandler<GetDistanceBetweenTwoAirportsQuery, GetDistanceBetweenTwoAirportsQueryResult>
{
    private const double EarthRadiusKm = 6371.0; // Radius of the Earth in kilometers
    private readonly HttpClient _httpClient;
    private readonly IMapper _mapper;
    private ILogger<GetAirportInfoByIataHandler> _logger;
    private readonly IAirportRepository _airportRepository;
    private readonly JsonSerializerOptions _jsonOptions;
    private readonly ICityRepository _cityRepository;
    private readonly ICountryRepository _countryRepository;
    private readonly ILocationRepository _locationRepository;

    public GetDistanceBetweenTwoAirportsHandler(HttpClient httpClient, IAirportRepository airportRepository, IMapper mapper,
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
    
    public async Task<GetDistanceBetweenTwoAirportsQueryResult> Handle(GetDistanceBetweenTwoAirportsQuery query, CancellationToken cancellationToken)
    {
        var firstAirport = await _airportRepository.GetAirportDataByIataAsync(query.firstAirportCode, cancellationToken);
        var secondAirport= await _airportRepository.GetAirportDataByIataAsync(query.secondAirportCode, cancellationToken);
        
        Location firstLocation = firstAirport == null ? (await CreateAirportInfoByIataAsync(query.firstAirportCode, cancellationToken)).Location : firstAirport.Location;
        Location secondLocation  = secondAirport == null ? (await CreateAirportInfoByIataAsync(query.secondAirportCode, cancellationToken)).Location : secondAirport.Location;
        
        var distance = CalculateDistance(firstLocation, secondLocation);

        return new GetDistanceBetweenTwoAirportsQueryResult(distance);
    }

    private async Task<Airport> CreateAirportInfoByIataAsync(string airportCode, CancellationToken cancellationToken)
    {
        (GetAirportInfoByIataResult? _, Airport? airport) = await AirportHelper.GetAirportDataFromApiAsync(_httpClient, airportCode, _mapper,
            _jsonOptions, cancellationToken);

        if (airport != null)
        {
            _logger.LogInformation("********* Creating new airport *****************");
            airport = await AirportHelper.SetAirportEntitiesAsync(airport,
                _countryRepository, _cityRepository, _locationRepository, cancellationToken);
            
            await _airportRepository.CreateAsync(airport, cancellationToken);    
        }
        
        return airport;
    }
    
    public static double CalculateDistance(Location first, Location second)
    {
        double lat1Rad = DegreesToRadians(first.Lat);
        double lon1Rad = DegreesToRadians(first.Lon);
        double lat2Rad = DegreesToRadians(second.Lat);
        double lon2Rad = DegreesToRadians(second.Lon);

        double dLat = lat2Rad - lat1Rad;
        double dLon = lon2Rad - lon1Rad;

        double a = Math.Sin(dLat / 2) * Math.Sin(dLat / 2) +
                   Math.Cos(lat1Rad) * Math.Cos(lat2Rad) *
                   Math.Sin(dLon / 2) * Math.Sin(dLon / 2);
        double c = 2 * Math.Atan2(Math.Sqrt(a), Math.Sqrt(1 - a));

        return Math.Round(EarthRadiusKm * c, 2);
    }

    /// <summary>
    /// Converts degrees to radians.
    /// </summary>
    private static double DegreesToRadians(double degrees)
    {
        return degrees * Math.PI / 180.0;
    }
}
