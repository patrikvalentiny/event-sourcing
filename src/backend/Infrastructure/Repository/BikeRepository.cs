using backend.Domain.Entities;
using backend.Domain.Events;
using backend.Domain.Events.BikeEvents;
using backend.Infrastructure.Context;
using Marten;
using Marten.Events;

namespace backend.Infrastructure.Repository;

public class BikeRepository(MartenContext martenContext)
{
    public async Task<IEnumerable<Bike>> GetAll()
    {
        using var session = martenContext.GetQuerySession();
        var bikes = await session.Query<Bike>().ToListAsync();
        return bikes;
    }



    
    public async Task<Bike?> Get(Guid id)
    {
        using var session = martenContext.GetQuerySession();
        var bikeStream = await session.Events.AggregateStreamAsync<Bike>(id);
        return bikeStream;
    }

       public async Task<Bike?> GetBySerialNumber(string serialNumber)
    {
        using var session = martenContext.GetQuerySession();
        var bike = await session.Query<Bike>().FirstOrDefaultAsync(b => b.SerialNumber == serialNumber);
        return bike;
    }

    public async Task Add(BikeRegistered bike)
    {
        using var session = martenContext.GetLightweightSession();
        session.Events.StartStream<Bike>(bike.Id, bike);
        await session.SaveChangesAsync();
    }


    public async Task Update(BikeUpdated bikeUpdated)
    {
        using var session = martenContext.GetLightweightSession();
        session.Events.Append(bikeUpdated.Id, bikeUpdated);
        await session.SaveChangesAsync();
    }

    public async Task<bool> Delete(BikeDeleted bikeDeleted)
    {
        using var session = martenContext.GetLightweightSession();
        session.Events.Append(bikeDeleted.Id, bikeDeleted);
        session.Events.ArchiveStream(bikeDeleted.Id);
        await session.SaveChangesAsync();
        return true;
    }

}
