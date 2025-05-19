using backend.Domain.Entities;

namespace backend.Domain.Events;

public record BikeRegistered(
    Guid Id,
    string Brand,
    string Model,
    string SerialNumber,
    int Year,
    BikeType BikeType
);

