using System.Text.Json.Serialization;
using Airport.Application.Dtos;

namespace Airport.API.Dtos;

public record GetAirportInfoByIataResponse()
{
    public string Iata { get; set; }
    
    public string Name { get; set; }
    
    public string City { get; set; }
    
    [JsonPropertyName("city_iata")]
    public string CityIata { get; set; }
    
    public string? Icao { get; set; }
    
    public string Country { get; set; }
    
    [JsonPropertyName("country_iata")]
    public string CountryIata { get; set; }

    public LocationDto Location { get; set; }
    
    public int Rating { get; set; }
    
    public int Hubs { get; set; }
    
    public string Type { get; set; }
    
    [JsonPropertyName("timezone_region_name")]
    public string TimezoneRegionName { get; set; }
}