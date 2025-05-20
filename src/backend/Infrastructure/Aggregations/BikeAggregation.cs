using System;
using backend.Domain.Entities;
using backend.Domain.Events;
using backend.Domain.Events.BikeEvents;
using Marten.Events.Aggregation;

namespace backend.Infrastructure.Aggregations;

public class BikeAggregation : SingleStreamProjection<Bike>
{
    public BikeAggregation()
    {
    }

    public Bike Create(BikeRegisteredEvent @event)
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
    public void Apply(BikeRegisteredEvent @event, Bike bike)
    {
        bike.Id = @event.Id;
        bike.Brand = @event.Brand;
        bike.Model = @event.Model;
        bike.SerialNumber = @event.SerialNumber;
        bike.Year = @event.Year;
        bike.BikeType = @event.BikeType;
    }

    public void Apply(BikeUpdated @event, Bike bike)
    {
        bike.Brand = @event.Brand;
        bike.Model = @event.Model;
        bike.SerialNumber = @event.SerialNumber;
        bike.Year = @event.Year;
        bike.BikeType = @event.BikeType;
    }

    public void Apply(BikeDeleted @event, Bike bike)
    {
        bike.IsDeleted = true;
    }

    public void Apply(RideLoggedEvent @event, Bike bike)
    {
        bike.TotalDistance += @event.Distance;
        bike.Components.ForEach(c =>
        {

            c.Mileage += @event.Distance;

        });
    }

    public void Apply(ComponentAddedEvent @event, Bike bike)
    {
        var component = new BikeComponent
        {
            ComponentId = @event.ComponentId,
            BikeId = bike.Id,
            ComponentType = @event.ComponentType,
            Brand = @event.Brand,
            Model = @event.Model,
            PurchaseDate = @event.PurchaseDate,
            Position = @event.Position,
            AddedAt = DateTime.UtcNow, // Consider using event timestamp if available
            Mileage = 0
        };
        bike.Components.Add(component);
    }

    public void Apply(ComponentReplacedEvent @event, Bike bike)
    {
        var oldComponent = bike.Components.FirstOrDefault(c => c.ComponentId == @event.OldComponentId);
        if (oldComponent != null)
        {
            bike.Components.Remove(oldComponent);
        }

        var newComponent = new BikeComponent
        {
            ComponentId = @event.NewComponentId,
            BikeId = bike.Id,
            ComponentType = @event.ComponentType,
            Brand = @event.Brand,
            Model = @event.Model,
            PurchaseDate = @event.PurchaseDate,
            Position = @event.Position,
            AddedAt = DateTime.UtcNow,
            Mileage = 0
        };
        bike.Components.Add(newComponent);
    }
}
