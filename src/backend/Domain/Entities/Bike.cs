using backend.Domain.Events;
using backend.Domain.Events.BikeEvents;

namespace backend.Domain.Entities;

public class Bike
{
    public Guid Id { get; set; }
    public string Brand { get; set; } = default!;
    public string Model { get; set; } = default!;
    public string SerialNumber { get; set; } = default!;
    public int Year { get; set; }
    public BikeType BikeType { get; set; }
    public bool IsDeleted { get; set; } = false;
    public double TotalDistance { get; set; } = 0;
}

public enum BikeType
{
    Road,
    Mountain,
    Gravel,
    City,
    Ebike,
    Hybrid
}
