using Airport.API.Dtos;
using Airport.Application.Airports.Queries;
using AutoMapper;

namespace Airport.API.Mapping;

public class AirportRequestProfile : Profile
{
    public AirportRequestProfile()
    {
        CreateMap<GetAirportInfoByIataResult, GetAirportInfoByIataResponse>();
    }
}