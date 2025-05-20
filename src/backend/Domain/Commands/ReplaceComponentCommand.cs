namespace backend.Domain.Commands;

public record ReplaceComponentCommand(
    Guid BikeId,
    Guid OldComponentId,
    string ComponentType,
    string Brand,
    string Model,
    DateTime PurchaseDate,
    string? Position = null
);
