namespace backend.Domain.Commands;

public record LogRideCommand(
    Guid BikeId,
    double Distance,
    DateTime RideDate
);
