using backend.Domain.Entities;
using backend.Domain.Events;
using backend.Infrastructure.Context;

namespace backend.Infrastructure.Repository;

public class ComponentRepository(MartenContext context)
{

    public IEnumerable<ComponentAdded> GetBikeComponents(Guid bikeId)
    {
        var session = context.GetQuerySession();
        var components = session.Events.QueryRawEventDataOnly<ComponentAdded>()
            .Where(x => x.BikeId == bikeId);
        return components;
    }

    public async Task<BikeComponent?> Get(Guid id)
    {
        var session = context.GetQuerySession();
        return await session.Events.AggregateStreamAsync<BikeComponent>(id);
    }

    public async Task Add(BikeComponent component)
    {
        var added = new ComponentAdded(
            component.Id,
            component.BikeId,
            component.ComponentType,
            component.Brand,
            component.Model,
            component.PurchaseDate,
            component.Position,
            component.AddedAt
        );
        var session = context.GetLightweightSession();
        session.Events.StartStream<BikeComponent>(component.Id, added);
        await session.SaveChangesAsync();
    }

    public async Task IncreaseDistance(Guid componentId, double mileage, Guid rideId )
    {
        var session = context.GetLightweightSession();
        session.Events.Append(componentId, new ComponentDistanceIncreased(componentId, mileage, rideId, DateTime.UtcNow));
        await session.SaveChangesAsync();
    }
}

