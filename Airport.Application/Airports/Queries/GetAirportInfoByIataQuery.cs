using System.Text.Json.Serialization;
using Airport.Application.Dtos;
using FluentValidation;
using MediatR;

namespace Airport.Application.Airports.Queries;

public record GetAirportInfoByIataQuery(string Iata) : IRequest<GetAirportInfoByIataResult?>;

public record GetAirportInfoByIataResult(
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

public class GetAirportInfoByIataValidator : AbstractValidator<GetAirportInfoByIataQuery>
{
    public GetAirportInfoByIataValidator()
    {
        RuleFor(x => x.Iata).NotNull().NotEmpty().WithMessage("Iata is required");
        RuleFor(x => x.Iata).MaximumLength(3).WithMessage("Iata must be maximum 3 symbols");
    }
}