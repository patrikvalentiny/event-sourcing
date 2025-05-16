using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace backend.Controllers;
[Route("api/[controller]")]
[ApiController]
public class BikeController : Controller
{
    [HttpGet]
    public IActionResult GetBikes()
    {
        // Simulate fetching bike data
        var bikes = new List<string> { "Bike1", "Bike2", "Bike3" };
        return Ok(bikes);
    }

    [HttpPost]
    public IActionResult AddBike([FromBody] string bike)
    {
        // Simulate adding a bike
        return CreatedAtAction(nameof(GetBikes), new { bike });
    }
}
