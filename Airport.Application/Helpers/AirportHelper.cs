using System.Text.Json;
using Airport.Application.Airports.Queries.GetAirportInfoByIata;
using Airport.Application.Interfaces;
using Airport.Domain.Entities;
using AutoMapper;

namespace Airport.Application.Helpers;

using Airport = Airport.Domain.Entities.Airport;

public class AirportHelper
{
    public static async Task<Airport> SetCityToAirportAsync(ICityRepository cityRepository, ICountryRepository countryRepository,
        Airport airport, CancellationToken cancellationToken)
    {
        var city = await cityRepository.GetByCityNameAsync(airport.City.Name, cancellationToken);
        if (city != null)
        {
            airport.CityId = city.Id;
            return airport;
        }
        
        if (string.IsNullOrWhiteSpace(airport.City.Name))
        {
            return airport;
        }

        var newCity = new City
        {
            Id = Guid.NewGuid(),
            Name = airport.City.Name,
            Iata = airport.City.Iata,
            CountryId = airport.Country.Id
        };
        
        airport.CityId = newCity.Id;
        airport.City = newCity;
        
        return airport;
    }

    public static async Task<Airport> SetCountryToAirportAsync(ICountryRepository countyRepository,
        Airport airport, CancellationToken cancellationToken)
    {
        var country = await countyRepository.GetByCountryNameAsync(airport.Country.Name, cancellationToken);
        if (country != null)
        {
            airport.Country = country;
            return airport;
        }
        
        if (string.IsNullOrWhiteSpace(airport.Country.Name))
        {
            return airport;
        }

        var newCountry = new Country
        {
            Id = Guid.NewGuid(),
            Name = airport.Country.Name,
            Iata = airport.Country.Iata
        };
        
        await countyRepository.CreateAsync(newCountry, cancellationToken);
        
        airport.Country = newCountry;
        
        return airport;
    }

    public static async Task<Airport> SetLocationToAirportAsync(ILocationRepository locationRepository,
        Airport airport, CancellationToken cancellationToken)
    {
        var location = await locationRepository.GetByCoordinatesAsync(airport.Location.Lon, airport.Location.Lat, cancellationToken);
        if (location != null)
        {
            airport.Location = location;
            
            return airport;
        }
        
        if (airport.Location.Lat == 0 || airport.Location.Lon == 0)
        {
            return airport;
        }

        var newLocation = new Location()
        {
            Id = Guid.NewGuid(),
            Lon = airport.Location.Lon,
            Lat = airport.Location.Lat
        };
        
        airport.Location = newLocation;
        
        return airport;
    }

    public static async Task<(GetAirportInfoByIataResult? airportDataResult, Airport? airport)> GetAirportDataFromApiAsync(
        HttpClient httpClient, string iata, IMapper mapper, JsonSerializerOptions jsonOptions, CancellationToken cancellationToken)
    {
        GetAirportInfoByIataResult? airportDataResult;
        Airport? airport;
        using (var request = new HttpRequestMessage(HttpMethod.Get, $"{httpClient.BaseAddress}/airports/{iata}"))
        {
            HttpResponseMessage response = await httpClient.SendAsync(request, cancellationToken);

            if (response.IsSuccessStatusCode)
            {
                var jsonResponse = await response.Content.ReadAsStringAsync(cancellationToken);
                
                airportDataResult = JsonSerializer.Deserialize<GetAirportInfoByIataResult>(jsonResponse, jsonOptions);
                
                airport = mapper.Map<Airport>(airportDataResult);
            }
            else
            {
                var errorMessage = $"Error: {response.StatusCode} - {response.ReasonPhrase}";
                throw new HttpRequestException(errorMessage);
            }
        }
        
        return (airportDataResult, airport);
    }
    
    public static async Task<Airport> SetAirportEntitiesAsync(Airport airport,
        ICountryRepository countryRepository, ICityRepository cityRepository, ILocationRepository locationRepository,
        CancellationToken cancellationToken)
    {
        airport = await AirportHelper.SetCountryToAirportAsync(countryRepository, airport, cancellationToken);
        airport = await AirportHelper.SetCityToAirportAsync(cityRepository, countryRepository, airport, cancellationToken);
        airport = await AirportHelper.SetLocationToAirportAsync(locationRepository, airport, cancellationToken);
    
        return airport;
    }
}