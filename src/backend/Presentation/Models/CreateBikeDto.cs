namespace backend.Presentation.Models;

public sealed record CreateBikeDto(
    string Brand,
    string Model,
    string SerialNumber,
    int Year,
    string BikeType
);