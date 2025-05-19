namespace backend.Presentation.Models;

public sealed record UpdateBikeDto(
    Guid Id,
    string Brand,
    string Model,
    string SerialNumber,
    int Year,
    string BikeType
);
