using backend.Domain.Commands;
using backend.Domain.Events;
using backend.Infrastructure.Repository;
using Serilog;

namespace backend.Application.Handlers;

public class ReplaceComponentHandler(ComponentRepository componentRepository)
{
    public async Task<Guid> Handle(ReplaceComponentCommand command)
    {
        var componentId = Guid.NewGuid(); // Generate a new ID for the new component
        var componentReplacedEvent = new ComponentReplacedEvent
        (
            command.BikeId,
            command.OldComponentId,
            componentId, // Generate a new ID for the new component
            command.ComponentType,
            command.Brand,
            command.Model,
            command.PurchaseDate,
            DateTime.UtcNow,
            command.Position
        );

        await componentRepository.Replace(command.BikeId, componentReplacedEvent);
        return componentId;
    }
}
