namespace backend.Domain.Events;

public record ComponentAddedEvent(
    Guid ComponentId,
    Guid BikeId,
    string ComponentType, // "Chain", "Tires"
    string Brand,
    string Model,
    DateTime PurchaseDate,
    string? Position, // Optional: "front", "rear" for tires
    DateTime AddedAt
);
