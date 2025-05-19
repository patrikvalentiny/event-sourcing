namespace backend.Domain.Events;

public record ComponentReplaced(
    Guid EventId,
    Guid BikeId,
    Guid OldComponentId,
    Guid NewComponentId,
    string ComponentType, // "Chain", "Tires"
    DateTime ReplacementDate
);
