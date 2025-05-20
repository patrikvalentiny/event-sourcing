using System.ComponentModel;

namespace backend.Domain.Entities;

public class Bike
{
    public Guid Id { get; set; }
    public string Brand { get; set; } = default!;
    public string Model { get; set; } = default!;
    public string SerialNumber { get; set; } = default!;
    public int Year { get; set; }
    public string BikeType { get; set; } = default!;
    public bool IsDeleted { get; set; } = false;
    public double TotalDistance { get; set; } = 0;
    public List<BikeComponent> Components { get; set; } = [];
}

