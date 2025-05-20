namespace backend.Domain.Entities;

public record Ride
{
    public Guid Id { get; init; }
    public Guid BikeId { get; init; }
    public double Distance { get; init; }
    public DateTime RideDate { get; init; }
    public DateTime AddedAt { get; init; } = DateTime.UtcNow;
}
