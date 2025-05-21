using Microsoft.AspNetCore.Mvc;
using backend.Domain.Commands;
using Wolverine;

namespace backend.Presentation.Controllers;

[Route("api/components/{bikeId}")]
[ApiController]
public class ComponentController(IMessageBus bus) : Controller
{


    [HttpGet("{id}")]
    public async Task<IActionResult> GetComponent(Guid id)
    {
        return NotFound();
    }

    [HttpPost]
    public async Task<IActionResult> AddComponent(Guid bikeId, [FromBody] AddComponentCommand component)
    {
        if( component.BikeId != bikeId)
            return BadRequest("Bike ID in the URL does not match the Bike ID in the request body.");
        if (component == null)
            return BadRequest("Component cannot be null");
        
        await bus.InvokeAsync(component);
        return Ok();
    }

    [HttpPut("{oldComponentId}")]
    public async Task<IActionResult> ReplaceComponent(Guid bikeId, Guid oldComponentId, [FromBody] ReplaceComponentCommand command)
    {
        if (command.BikeId != bikeId)
            return BadRequest("Bike ID in the URL does not match the Bike ID in the request body.");
        
        if (command.OldComponentId != oldComponentId)
            return BadRequest("Old Component ID in the URL does not match the Old Component ID in the request body.");

        if (command == null)
            return BadRequest("Command cannot be null");

        var newId=  await bus.InvokeAsync<Guid>(command);
        return Ok(newId);
    }
}