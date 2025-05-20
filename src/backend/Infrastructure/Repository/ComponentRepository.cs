using System;
using backend.Domain.Events;
using backend.Infrastructure.Context;
using Marten;

namespace backend.Infrastructure.Repository;

public class ComponentRepository(MartenContext martenContext)
{
    public async Task Create(Guid bikeId, ComponentAddedEvent componentAddedEvent)
    {
        using var session = martenContext.GetLightweightSession();
        session.Events.Append(bikeId, componentAddedEvent);
        await session.SaveChangesAsync();
    }

    public async Task Replace(Guid bikeId, ComponentReplacedEvent componentReplacedEvent)
    {
        using var session = martenContext.GetLightweightSession();
        session.Events.Append(bikeId, componentReplacedEvent);
        await session.SaveChangesAsync();
    }
}
