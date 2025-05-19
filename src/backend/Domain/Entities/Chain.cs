namespace backend.Domain.Entities;

public class Chain
{
    public Guid Id { get; set; }
    public Guid BikeId { get; set; }
    public string Brand { get; set; } = default!;
    public string Model { get; set; } = default!;
    public DateTime PurchaseDate { get; set; }
    public double Distance { get; set; }
}
