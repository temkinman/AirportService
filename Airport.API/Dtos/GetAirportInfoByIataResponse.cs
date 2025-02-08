using System.Text.Json.Serialization;
using Airport.Application.Dtos;

namespace Airport.API.Dtos;

public record GetAirportInfoByIataResponse()
{
    [JsonPropertyName("iata")]
    public string Iata { get; set; }
    
    [JsonPropertyName("name")]
    public string Name { get; set; }
    
    [JsonPropertyName("city")]
    public string City { get; set; }
    
    [JsonPropertyName("city_iata")]
    public string CityIata { get; set; }
    
    [JsonPropertyName("icao")]
    public string? Icao { get; set; }
    
    [JsonPropertyName("country")]
    public string Country { get; set; }
    
    [JsonPropertyName("country_iata")]
    public string CountryIata { get; set; }

    [JsonPropertyName("location")]
    public LocationDto Location { get; set; }
    
    [JsonPropertyName("rating")]
    public int Rating { get; set; }
    
    [JsonPropertyName("hubs")]
    public int Hubs { get; set; }
    
    [JsonPropertyName("type")]
    public string Type { get; set; }
    
    [JsonPropertyName("timezone_region_name")]
    public string TimezoneRegionName { get; set; }
}