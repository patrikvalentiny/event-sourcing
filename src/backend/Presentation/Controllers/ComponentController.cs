using Microsoft.AspNetCore.Mvc;
using backend.Application.Service;
using backend.Domain.Entities;

namespace backend.Presentation.Controllers;

[Route("api/components/{bikeId}")]
[ApiController]
public class ComponentController(ComponentService componentService) : Controller
{
    [HttpGet]
    public async Task<IActionResult> GetComponents(Guid bikeId)
    {
        var components = await componentService.GetAllComponents(bikeId);
        return Ok(components);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetComponent(Guid id)
    {
        var component = await componentService.GetComponent(id);
        if (component == null)
            return NotFound();
        return Ok(component);
    }

    [HttpPost]
    public async Task<IActionResult> AddComponent(Guid bikeId, [FromBody] BikeComponent component)
    {
        component.BikeId = bikeId;
        // Ensure AddedAt is set if not handled by domain or repository
        if (component.AddedAt == default)
        {
            component.AddedAt = DateTime.UtcNow;
        }
        await componentService.AddComponent(component);
        return Ok();
    }


    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateComponent(Guid id, [FromBody] BikeComponent component)
    {
        if (id != component.Id)
            return BadRequest();
        await componentService.UpdateComponent(component);
        return NoContent();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteComponent(Guid id)
    {
        var deleted = await componentService.DeleteComponent(id);
        if (!deleted)
            return NotFound();
        return NoContent();
    }
}

// Helper class for the request body of IncreaseDistance
public class IncreaseDistanceRequest
{
    public int Mileage { get; set; }
    public Guid RideId { get; set; }
}
