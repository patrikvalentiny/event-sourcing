namespace backend.Domain.Events;

public record ComponentReplaced(
    Guid Id,
    Guid BikeId,
    string ComponentType, // "Chain", "Tires"
    DateTime ReplacementDate
);
