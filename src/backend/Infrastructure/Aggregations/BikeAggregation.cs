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

    public Bike Create(BikeRegistered @event)
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
    public void Apply(BikeRegistered @event, Bike bike)
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
    
    public void Apply(RideLogged @event, Bike bike)
    {
        bike.TotalDistance += @event.Distance;
    }
}
