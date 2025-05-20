using backend.Domain.Commands;
using Microsoft.AspNetCore.Mvc;
using Wolverine;

namespace backend.Application.Service;

[Route("api/rides")]
[ApiController]
public class RidesController(IMessageBus bus) : ControllerBase
{
    // add ride
    [HttpPost]
    public async Task<IActionResult> AddRide([FromBody] LogRideCommand ride)
    {
        if (ride == null)
            return BadRequest("Ride cannot be null");

        // Validate the ride object here if necessary

        var streamId = await bus.InvokeAsync<Guid>(ride);
        return CreatedAtAction(nameof(GetRide), new { id = streamId }, ride);   
    }

    // get ride by id
    [HttpGet("{id:guid}")]
    public async Task<IActionResult> GetRide([FromRoute] Guid id)
    {
        // TODO: Implement retrieval logic using CQRS and event sourcing
        // For now, return NotFound to satisfy the compiler
        return NotFound();
    }
}

