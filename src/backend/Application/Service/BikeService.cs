using System;
using backend.Domain.Entities;
using backend.Infrastructure.Repository;

namespace backend.Application.Service;

public class BikeService(BikeRepository bikeRepository)
{
    public async Task<IEnumerable<Bike>> GetAllBikes()
    {
        return await bikeRepository.GetAll();
    }

    public async Task RegisterBike(string brand, string model, string serialNumber, int year, string bikeType)
    {
        var bike = new Bike()
        {
            Id = Guid.NewGuid(),
            Brand = brand,
            Model = model,
            SerialNumber = serialNumber,
            Year = year,
            BikeType = Enum.Parse<BikeType>(bikeType)
        };
        await bikeRepository.Add(bike);
    }

    public async Task<Bike?> GetBike(Guid id)
    {
        return await bikeRepository.Get(id);
    }

    public async Task UpdateBike(Bike bike)
    {
        await bikeRepository.Update(bike);
    }

    public async Task<bool> DeleteBike(Guid id)
    {
        return await bikeRepository.Delete(id);
    }
}
