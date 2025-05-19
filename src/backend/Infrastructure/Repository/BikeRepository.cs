using System;
using System.Threading.Tasks;
using backend.Domain.Entities;
using backend.Infrastructure.Context;
using Marten;

namespace backend.Infrastructure.Repository;

public class BikeRepository(MartenContext martenContext)
{
    public async Task<IEnumerable<Bike>> GetAll()
    {
        using var session = martenContext.GetQuerySession();
        var bikes = await session.Query<Bike>().ToListAsync();
        return bikes;
    }

    public async Task<Bike?> GetBySerialNumber(string serialNumber)
    {
        using var session = martenContext.GetQuerySession();
        var bike = await session.Query<Bike>().FirstOrDefaultAsync(b => b.SerialNumber == serialNumber);
        return bike;
    }

    public async Task Add(Bike bike)
    {
        using var session = martenContext.GetLightweightSession();
        session.Store(bike);
        await session.SaveChangesAsync();
    }

    public async Task<Bike?> Get(Guid id)
    {
        using var session = martenContext.GetQuerySession();
        var bike = await session.LoadAsync<Bike>(id);
        return bike;
    }

    public async Task Update(Bike bike)
    {
        using var session = martenContext.GetLightweightSession();
        session.Update(bike);
        await session.SaveChangesAsync();
    }

    public async Task<bool> Delete(Guid id)
    {
        using var session = martenContext.GetLightweightSession();
        var bike = await session.LoadAsync<Bike>(id);
        if (bike != null)
        {
            session.Delete(bike);
            await session.SaveChangesAsync();
            return true;
        }
        return false;
    }

}
