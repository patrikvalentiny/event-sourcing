namespace backend.Domain.Events;

public record ComponentDistanceIncreased(
    Guid ComponentId,
    double DistanceAdded,
    Guid RideId,
    DateTime UpdatedAt
);
