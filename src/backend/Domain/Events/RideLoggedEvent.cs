namespace backend.Domain.Events;

public record RideLoggedEvent(
    Guid BikeId,
    Guid RideId,
    double Distance,
    DateTime RideDate,
    DateTime LoggedAt
);
