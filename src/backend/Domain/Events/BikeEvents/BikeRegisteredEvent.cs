namespace backend.Domain.Events;

public record BikeRegisteredEvent(
    Guid Id,
    string Brand,
    string Model,
    string SerialNumber,
    int Year,
    string BikeType
);

