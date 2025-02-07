using Airport.Application.Airports.Queries;
using Airport.Application.Dtos;
using Airport.Domain.Entities;
using Airport.Domain.Enums;
using AutoMapper;

namespace Airport.Application.Mapping;

using Airport = Domain.Entities.Airport;
public class AirportMappingProfile : Profile
{
    public AirportMappingProfile()
    {
        CreateMap<Airport, GetAirportInfoByIataResult>()
            .ForMember(dest => dest.City, opt => opt.MapFrom(src => src.City.Name))
            .ForMember(dest => dest.CityIata, opt => opt.MapFrom(src => src.City.Iata))
            .ForMember(dest => dest.Country, opt => opt.MapFrom(src => src.Country.Name))
            .ForMember(dest => dest.CountryIata, opt => opt.MapFrom(src => src.Country.Iata));

        CreateMap<GetAirportInfoByIataResult, Airport>()
            .ForMember(dest => dest.City, opt => opt.MapFrom(src => new City 
            { 
                Name = src.City, 
                Iata = src.CityIata 
            }))
            .ForMember(dest => dest.Country, opt => opt.MapFrom(src => new Country 
            { 
                Name = src.Country, 
                Iata = src.CountryIata 
            }))
            // .ForMember(dest => dest.City.Name, opt => opt.MapFrom(src => src.City))
            // .ForMember(dest => dest.City.Iata, opt => opt.MapFrom(src => src.CityIata))
            // .ForMember(dest => dest.Country.Name, opt => opt.MapFrom(src => src.Country))
            // .ForMember(dest => dest.Country.Iata, opt => opt.MapFrom(src => src.CountryIata))
            .ForMember(dest => dest.Type, opt => opt.MapFrom<BuildingTypeResolver>());

        // CreateMap<GetAirportInfoByIataResult, City>()
        //     .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.City))
        //     .ForMember(dest => dest.Iata, opt => opt.MapFrom(src => src.CityIata));
        //
        // CreateMap<GetAirportInfoByIataResult, Country>()
        //     .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Country))
        //     .ForMember(dest => dest.Iata, opt => opt.MapFrom(src => src.CountryIata));
        
        CreateMap<Location, LocationDto>().ReverseMap();
    }
}

public class BuildingTypeResolver : IValueResolver<GetAirportInfoByIataResult, Airport, BuildingType>
{
    public BuildingType Resolve(GetAirportInfoByIataResult source, Airport destination, BuildingType destMember, ResolutionContext context)
    {
        return Enum.TryParse<BuildingType>(source.Type, true, out var buildingType) ? buildingType : BuildingType.airport;
    }
}