using System.Text.Json.Serialization;
using Airport.Application.Dtos;

namespace Airport.API.Dtos;

public record GetAirportInfoByIataResponse(
    string Iata,
    string? Icao,
    string Name,
    int Rating,
    int Hubs,
    string Type,
    string City,
    string Country,
    LocationDto Location)
{
    [JsonPropertyName("timezone_region_name")]
    public string TimezoneRegionName { get; set; }  
    [JsonPropertyName("city_iata")]
    public string CityIata { get; set; }
    
    [JsonPropertyName("country_iata")]
    public string CountryIata { get; set; }
}