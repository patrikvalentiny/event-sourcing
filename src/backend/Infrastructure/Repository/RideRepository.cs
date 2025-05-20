using backend.Domain.Events;
using backend.Infrastructure.Context;

namespace backend.Infrastructure.Repository;

public class RideRepository(MartenContext context)
{
    public async Task<bool> AddRideToBike(RideLoggedEvent ride)
    {
        using var session = context.GetLightweightSession();
        session.Events.Append(ride.BikeId, ride);
        await session.SaveChangesAsync();
        return true;
    }

}
