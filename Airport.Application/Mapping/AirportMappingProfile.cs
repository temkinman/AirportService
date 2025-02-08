using Airport.Application.Airports.Queries.GetAirportInfoByIata;
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
        CreateMap<City, CityDto>();
        CreateMap<Country, CountryDto>();
        CreateMap<Location, LocationDto>().ReverseMap();

        CreateMap<Airport, GetAirportInfoByIataResult>()
            .ForMember(dest => dest.CityDto, opt => opt.MapFrom(src => new CityDto{ Iata = src.Iata, Name = src.Name }))
            .ForMember(dest => dest.CountryDto, opt => opt.MapFrom(src => new CountryDto{ Iata = src.Iata, Name = src.Name }))
            .ForMember(dest => dest.City, opt => opt.MapFrom(src => src.City.Name))
            .ForMember(dest => dest.Country, opt => opt.MapFrom(src => src.Country.Name))
            .ForMember(dest => dest.Location,
                opt => opt.MapFrom(src => new LocationDto(src.Location.Lon, src.Location.Lat)));

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
            .ForMember(dest => dest.CityId, opt => opt.Ignore())
            .ForMember(dest => dest.LocationId, opt => opt.Ignore())
            .ForMember(dest => dest.Id, opt => opt.Ignore())
            .ForMember(dest => dest.Type, opt => opt.MapFrom<BuildingTypeResolver>());
    }
}

public class BuildingTypeResolver : IValueResolver<GetAirportInfoByIataResult, Airport, BuildingType>
{
    public BuildingType Resolve(GetAirportInfoByIataResult source, Airport destination, BuildingType destMember, ResolutionContext context)
    {
        return Enum.TryParse<BuildingType>(source.Type, true, out var buildingType) ? buildingType : BuildingType.airport;
    }
}