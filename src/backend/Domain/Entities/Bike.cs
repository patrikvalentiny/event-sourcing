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

    public static Bike Create(BikeRegistered @event)
    {
        return new Bike
        {
            Id = @event.Id,
            Brand = @event.Brand,
            Model = @event.Model,
            SerialNumber = @event.SerialNumber,
            Year = @event.Year,
            BikeType = @event.BikeType
        };
    }
    public static Bike Apply(BikeUpdated @event)
    {
        return new Bike
        {
            Id = @event.Id,
            Brand = @event.Brand,
            Model = @event.Model,
            SerialNumber = @event.SerialNumber,
            Year = @event.Year,
            BikeType = @event.BikeType
        };
    }
    public static Bike Apply(BikeDeleted @event)
    {
        return new Bike
        {
            Id = @event.Id,
            IsDeleted = true
        };
    }
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
