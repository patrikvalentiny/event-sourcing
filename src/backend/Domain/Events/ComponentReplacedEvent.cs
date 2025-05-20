namespace backend.Domain.Events;

public record ComponentReplacedEvent(
    Guid BikeId,
    Guid OldComponentId,
    Guid NewComponentId,
    string ComponentType,
    string Brand,
    string Model,
    DateTime PurchaseDate,
    DateTime AddedAt,
    string? Position = null
);
