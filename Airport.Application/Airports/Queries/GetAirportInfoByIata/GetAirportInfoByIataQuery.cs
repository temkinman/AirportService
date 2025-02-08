using System.Text.Json.Serialization;
using Airport.Application.Dtos;
using Airport.Application.Helpers;
using FluentValidation;
using MediatR;

namespace Airport.Application.Airports.Queries.GetAirportInfoByIata;

public record GetAirportInfoByIataQuery(string Iata) : IRequest<GetAirportInfoByIataResult?>;

public record GetAirportInfoByIataResult
{
    public string Iata { get; set; }
    public string? Icao { get; set; }
    public string Name { get; set; }
    public string City { get; set; }
    
    [JsonPropertyName("city_iata")]
    public string CityIata { get; set; }
    public string Country { get; set; }
    
    [JsonPropertyName("country_iata")]
    public string CountryIata { get; set; }
    public int Rating { get; set; }
    public int Hubs { get; set; }
    public string Type { get; set; }
    
    [JsonPropertyName("timezone_region_name")]
    public string TimezoneRegionName { get; set; }
    public LocationDto Location { get; set; }
    public CityDto CityDto { get; set; }
    public CountryDto CountryDto { get; set; }
}

public class GetAirportInfoByIataValidator : AbstractValidator<GetAirportInfoByIataQuery>
{
    public GetAirportInfoByIataValidator()
    {
        RuleFor(x => x.Iata).NotNull().NotEmpty().WithMessage("Iata is required");
        RuleFor(x => x.Iata).MaximumLength(3).WithMessage("Iata must be maximum 3 symbols");
        RuleFor(x => x.Iata).Must(AirportHelper.BeUpperCase).WithMessage("The string must be in uppercase.");
    }
}