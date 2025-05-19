namespace backend.Domain.Events;

public record MaintenanceLogged(
    Guid EventId,
    Guid BikeId,
    Guid? ComponentId, // Optional
    string ActivityType, // e.g., "Cleaned", "Replaced", "Adjusted"
    string Description,
    DateTime DatePerformed,
    string? Notes, // Optional
    DateTime LoggedAt
);
