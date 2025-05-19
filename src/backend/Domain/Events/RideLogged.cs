namespace backend.Domain.Events;

public record RideLogged(
    Guid EventId,
    Guid BikeId,
    double Distance,
    DateTime RideDate,
    DateTime LoggedAt
);
