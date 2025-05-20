using backend.Domain.Commands;
using backend.Domain.Events;
using backend.Infrastructure.Repository;
using Serilog;

namespace backend.Application.Handlers;

public class AddComponentHandler(ComponentRepository componentRepository)
{
    public async Task Handle(AddComponentCommand command)
    {
        var componentAddedEvent = new ComponentAddedEvent
        (
            Guid.NewGuid(), // Generate a new ID for the component
            command.BikeId,
            command.ComponentType,
            command.Brand,
            command.Model,
            command.PurchaseDate,
            command.Position,
            DateTime.UtcNow
        );

        await componentRepository.Create(command.BikeId, componentAddedEvent);
        
        Log.Debug($"Component added to bike {command.BikeId}: {command.ComponentType} {command.Brand} {command.Model}");
    }
}
