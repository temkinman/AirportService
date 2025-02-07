using Airport.API.Dtos;
using Airport.Application.Airports.Queries;
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
    // [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(PatientDto))]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<ActionResult<GetAirportInfoByIataResponse>> GetAirportInfoByIata(string iata)
    {
        var result = await _mediator.Send(new GetAirportInfoByIataQuery(iata));
        var response = _mapper.Map<GetAirportInfoByIataResponse>(result);

        return Ok(response);
    }
}