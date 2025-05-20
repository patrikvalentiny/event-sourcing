using System.ComponentModel;
using backend.Domain.Entities;
using backend.Domain.Events;
using Marten.Events.Aggregation;

namespace backend.Infrastructure.Aggregations;

public class ComponentAggregation : SingleStreamProjection<BikeComponent>
{

    public BikeComponent Create(ComponentAdded @event)
    {
        return new BikeComponent
        {
            Id = @event.Id,
            BikeId = @event.BikeId,
            ComponentType = @event.ComponentType,
            Brand = @event.Brand,
            Model = @event.Model,
            PurchaseDate = @event.PurchaseDate,
            Position = @event.Position,
            AddedAt = @event.AddedAt
        };
    }

    public void Apply(ComponentAdded @event, BikeComponent component)
    {
        component.Id = @event.Id;
        component.BikeId = @event.BikeId;
        component.ComponentType = @event.ComponentType;
        component.Brand = @event.Brand;
        component.Model = @event.Model;
        component.PurchaseDate = @event.PurchaseDate;
        component.Position = @event.Position;
        component.AddedAt = @event.AddedAt;
    }
    
    public void Apply(ComponentDistanceIncreased @event, BikeComponent component)
    {
        component.Mileage += @event.DistanceAdded;
    }
}
