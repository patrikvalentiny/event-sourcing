namespace backend.Domain.Events;

public record ComponentAdded(
    Guid Id,
    Guid BikeId,
    string ComponentType, // "Chain", "Tires"
    string Brand,
    string Model,
    DateTime PurchaseDate,
    string? Position, // Optional: "front", "rear" for tires
    DateTime AddedAt
);
