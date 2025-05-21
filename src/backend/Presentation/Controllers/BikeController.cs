using Microsoft.AspNetCore.Mvc;
using backend.Application.Service;
using backend.Domain.Commands;
using Wolverine;
using backend.Domain.Entities;
using backend.Application.Queries;

namespace backend.Presentation.Controllers;

[Route("api/bikes")]
[ApiController]
public class BikeController( IMessageBus bus) : Controller
{
    // [HttpGet]
    // public async Task<IActionResult> GetBikes()
    // {
    //     // var bikes = await bikeService.GetAllBikes();
    //     // return Ok(bikes);
    //     return Ok()
    // }

    [HttpGet]
    public async Task<IActionResult> GetBike([FromBody] GetBikeQuery query)
    {
        var bike = await bus.InvokeAsync<Bike?>(query);
        if (bike == null)
            return NotFound();
        return Ok(bike);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetBike(Guid id)
    {
        var query = new GetBikeQuery { BikeId = id };
        return await GetBike(query);
    }
    [HttpPost]
    public async Task<IActionResult> AddBike([FromBody] RegisterBikeCommand bikeCommand)
    {
        var streamId = await bus.InvokeAsync<Guid>(bikeCommand);
        return CreatedAtAction(nameof(GetBike), new { id = streamId }, bikeCommand);
    }
}
