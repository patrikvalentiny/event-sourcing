using backend.Domain.Commands;
using backend.Domain.Events;
using backend.Infrastructure.Repository;
using Serilog;

namespace backend.Application.Handlers;

public class RegisterBikeHandler(BikeRepository bikeRepository)
{
    public async Task<Guid> Handle(RegisterBikeCommand command)
    {
        var bikeRegisteredEvent = new BikeRegisteredEvent
        (
            Guid.NewGuid(),
            command.Brand,
            command.Model,
            command.SerialNumber,
            command.Year,
            command.BikeType
        );

        var streamId = await bikeRepository.Add(bikeRegisteredEvent);
        // Here you would typically save the bike to a database or perform other actions
        Log.Debug($"Bike registered: {command.Brand} {command.Model}, Serial Number: {command.SerialNumber}, Year: {command.Year}, Type: {command.BikeType}");
        return streamId;
    }

}
