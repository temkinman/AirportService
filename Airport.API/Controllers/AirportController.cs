using Microsoft.AspNetCore.Mvc;

namespace Airport.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class AirportController : ControllerBase
{
    [HttpGet]
    [Route("airports/{iata}")]
    // [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(PatientDto))]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<ActionResult> GetAirportInfoByIata(string iata)
    {
        // var result = await _mediator.Send(new GetPatientByIdQuery(patientId));
        // var response = _mapper.Map<PatientDto>(result.Patient);

        return Ok("HO-HO-HO");
    }
}