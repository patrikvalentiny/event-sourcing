using backend.Domain.Entities;
using backend.Domain.Events;
using backend.Infrastructure.Context;

namespace backend.Infrastructure.Repository;

public class BikeRepository(MartenContext martenContext)
{

    public async Task<Bike?> Get(Guid id)
    {
        using var session = martenContext.GetQuerySession();
        var bikeStream = await session.Events.AggregateStreamAsync<Bike>(id);
        return bikeStream;
    }

    public async Task<Guid> Add(BikeRegisteredEvent bike)
    {
        using var session = martenContext.GetLightweightSession();
        var streamId = session.Events.StartStream<Bike>(bike.Id, bike).Id;
        await session.SaveChangesAsync();
        return streamId;
    }


}
