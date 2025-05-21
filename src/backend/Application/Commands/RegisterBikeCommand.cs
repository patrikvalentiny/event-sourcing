namespace backend.Domain.Commands;

public record RegisterBikeCommand(
    string Brand,
    string Model,
    string SerialNumber,
    int Year,
    string BikeType
);

