using System;

namespace backend.Domain.Entities;

public class BikeComponent
{
    public Guid ComponentId { get; set; }
    public Guid BikeId { get; set; }
    public string ComponentType { get; set; } = default!; // "Chain", "Tires"
    public string Brand { get; set; } = default!;
    public string Model { get; set; } = default!;
    public DateTime PurchaseDate { get; set; }
    public string? Position { get; set; } // Optional: "front", "rear" for tires
    public DateTime AddedAt { get; set; }
    public double Mileage { get; set; } // Total distance covered by the component

}
