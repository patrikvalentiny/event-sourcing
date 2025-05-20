using backend.Domain.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace backend.Application.Service;

[Route("api/rides")]
[ApiController]
public class RidesController(RideService service) : ControllerBase
{
    // add ride
    [HttpPost]
    public async Task<IActionResult> AddRide([FromBody] Ride ride)
    {
        // Logic to add a ride
        await service.AddRide(ride);
        return Ok();
    }
}

