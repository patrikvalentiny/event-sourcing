using System;
using backend.Domain.Entities;
using backend.Domain.Events;
using backend.Domain.Events.BikeEvents;
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
        var bikeRegistered = new BikeRegisteredEvent(
            Guid.NewGuid(),
            brand,
            model,
            serialNumber,
            year,
            bikeType
        );
        await bikeRepository.Add(bikeRegistered);
    }

    public async Task<Bike?> GetBike(Guid id)
    {
        var bike = await bikeRepository.Get(id);
        return bike;
    }

    public async Task UpdateBike(Bike bike)
    {
        var bikeUpdated = new BikeUpdated(
            bike.Id,
            bike.Brand,
            bike.Model,
            bike.SerialNumber,
            bike.Year,
            bike.BikeType
        );
        await bikeRepository.Update(bikeUpdated);
    }

    public async Task<bool> DeleteBike(Guid id)
    {
        var bikeDeleted = new BikeDeleted(id);
        return await bikeRepository.Delete(bikeDeleted);
    }

}
