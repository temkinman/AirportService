using Airport.API.Dtos;
using Airport.Application.Airports.Queries.GetAirportInfoByIata;
using Airport.Application.Airports.Queries.GetDistanceBetweenTwoAirports;
using AutoMapper;

namespace Airport.API.Mapping;

public class AirportRequestProfile : Profile
{
    public AirportRequestProfile()
    {
        CreateMap<GetAirportInfoByIataResult, GetAirportInfoByIataResponse>()
            .ForMember(dest => dest.Country, 
                opt => opt.MapFrom(src => string.IsNullOrWhiteSpace(src.Country) ? src.CountryDto.Name : src.Country))
            .ForMember(dest => dest.CountryIata, 
                opt => opt.MapFrom(src => string.IsNullOrWhiteSpace(src.CountryIata) ? src.CountryDto.Iata : src.CountryIata))
            .ForMember(dest => dest.City, 
                opt => opt.MapFrom(src => string.IsNullOrWhiteSpace(src.City) ? src.CityDto.Name : src.City))
            .ForMember(dest => dest.CityIata, opt =>
                opt.MapFrom(src => string.IsNullOrWhiteSpace(src.CityIata) ? src.CityDto.Name : src.CityIata));
        
        CreateMap<GetDistanceBetweenTwoAirportsQueryResult ,GetDistanceBetweenTwoAirportsResponse>();
    }
}