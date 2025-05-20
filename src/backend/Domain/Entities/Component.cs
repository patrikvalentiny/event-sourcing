using System;

namespace backend.Domain.Entities;

public class BikeComponent
{
    public Guid Id { get; set; }
    public Guid BikeId { get; set; }
    public string ComponentType { get; set; } // "Chain", "Tires"
    public string Brand { get; set; }
    public string Model { get; set; }
    public DateTime PurchaseDate { get; set; }
    public string? Position { get; set; } // Optional: "front", "rear" for tires
    public DateTime AddedAt { get; set; }
    public double Mileage { get; set; } = 0; // Total distance covered by the component

}
