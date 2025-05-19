namespace backend.Domain.Events;

public record ComponentDistanceIncreased(
    Guid EventId,
    Guid ComponentId,
    double DistanceAdded,
    Guid RideId,
    DateTime UpdatedAt
);
