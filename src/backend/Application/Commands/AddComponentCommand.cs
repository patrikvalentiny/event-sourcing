namespace backend.Domain.Commands;

public record class AddComponentCommand(
    Guid BikeId,
    string ComponentType,
    string Brand,
    string Model,
    DateTime PurchaseDate,
    string? Position = null
);
