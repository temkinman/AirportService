using Airport.API.Dtos;
using Airport.Application.Airports.Queries;
using Airport.Application.Airports.Queries.GetAirportInfoByIata;
using Airport.Application.Airports.Queries.GetDistanceBetweenTwoAirports;
using Airport.Application.Dtos;
using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Airport.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class AirportController : ControllerBase
{
    private readonly ILogger<AirportController> _logger;
    private readonly IMediator _mediator;
    private readonly IMapper _mapper;
    public AirportController(ILogger<AirportController> logger, IMediator mediator, IMapper mapper)
    {
        _logger = logger;
        _mediator = mediator;
        _mapper = mapper;
    }
    
    [HttpGet]
    [Route("airports/{iata}")]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<ActionResult<GetAirportInfoByIataResponse>> GetAirportInfoByIata(string iata)
    {
        var result = await _mediator.Send(new GetAirportInfoByIataQuery(iata));
        var response = _mapper.Map<GetAirportInfoByIataResponse>(result);

        return Ok(response);
    }
    
    [HttpGet]
    [Route("airports/distance")]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<ActionResult<GetDistanceBetweenTwoAirportsResponse>> GetDistanceBetweenAirports(string firstAirportCode, string secondAirportCode)
    {
        var result = await _mediator.Send(new GetDistanceBetweenTwoAirportsQuery(firstAirportCode, secondAirportCode));
        var response = _mapper.Map<GetDistanceBetweenTwoAirportsResponse>(result);
    
        return Ok(response);
    }
}