using System;
using backend.Domain.Commands;
using backend.Domain.Events;
using backend.Infrastructure.Repository;

namespace backend.Application.Handlers;

public class LogRideCommandHandler(RideRepository repository)
{
    public async Task<Guid> Handle(LogRideCommand command)
    {
        var rideId = Guid.NewGuid();
        var rideLoggedEvent = new RideLoggedEvent(
            command.BikeId,
            rideId,
            command.Distance,
            command.RideDate,
            DateTime.UtcNow
        );

        await repository.AddRideToBike(rideLoggedEvent);
        return rideId;
    }

}
