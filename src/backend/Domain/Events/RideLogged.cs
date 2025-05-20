namespace backend.Domain.Events;

public record RideLogged(
    Guid Id,
    double Distance,
    DateTime RideDate,
    DateTime LoggedAt
);
