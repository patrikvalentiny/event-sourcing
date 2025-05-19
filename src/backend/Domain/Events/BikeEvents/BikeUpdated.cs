using backend.Domain.Entities;

namespace backend.Domain.Events.BikeEvents;

public record class BikeUpdated(
    Guid Id,
    string Brand,
    string Model,
    string SerialNumber,
    int Year,
    BikeType BikeType
);
