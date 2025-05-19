using Microsoft.AspNetCore.Mvc;
using backend.Application.Service;
using backend.Domain.Entities;
using backend.Presentation.Models;

namespace backend.Presentation.Controllers;

[Route("api/bikes")]
[ApiController]
public class BikeController(BikeService bikeService) : Controller
{
    [HttpGet]
    public async Task<IActionResult> GetBikes()
    {
        var bikes = await bikeService.GetAllBikes();
        return Ok(bikes);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetBike(Guid id)
    {
        var bike = await bikeService.GetBike(id);
        if (bike == null)
            return NotFound();
        return Ok(bike);
    }

    [HttpPost]
    public async Task<IActionResult> AddBike([FromBody] CreateBikeDto bikeDto)
    {
        await bikeService.RegisterBike(
            bikeDto.Brand,
            bikeDto.Model,
            bikeDto.SerialNumber,
            bikeDto.Year,
            bikeDto.BikeType
        );
        return Ok();
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateBike(Guid id, [FromBody] UpdateBikeDto updateBike)
    {
        if (id != updateBike.Id)
            return BadRequest();
            
        var bike = new Bike()
        {
            Id = updateBike.Id,
            Brand = updateBike.Brand,
            Model = updateBike.Model,
            SerialNumber = updateBike.SerialNumber,
            Year = updateBike.Year,
            BikeType = Enum.Parse<BikeType>(updateBike.BikeType)
        };

        await bikeService.UpdateBike(bike);
        return NoContent();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteBike(Guid id)
    {
        var deleted = await bikeService.DeleteBike(id);
        if (!deleted)
            return NotFound();
        return NoContent();
    }
}
