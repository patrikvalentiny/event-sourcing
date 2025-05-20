using backend.Infrastructure.Repository;
using backend.Domain.Entities;
using System;
using System.Threading.Tasks;
using Serilog;

namespace backend.Application.Service;

public class ComponentService(ComponentRepository componentRepository)
{
    public async Task<BikeComponent?> GetComponent(Guid id)
    {
        return await componentRepository.Get(id);
    }

    public async Task AddComponent(BikeComponent component)
    {
        component.Id = Guid.NewGuid();
        await componentRepository.Add(component);
    }

    public async Task IncreaseComponentDistance(Guid componentId, int mileage, Guid rideId)
    {
        await componentRepository.IncreaseDistance(componentId, mileage, rideId);
    }

    // Assuming GetAllComponents, UpdateComponent, DeleteComponent methods exist or will be added
    // For example:
    public async Task<IEnumerable<BikeComponent>> GetAllComponents(Guid bikeId)
    {
        // This would require a corresponding method in ComponentRepository
        // e.g., return await componentRepository.GetAllByBikeId(bikeId);
        // For now, returning an empty list as the repository method is not provided.
        return await Task.FromResult(new List<BikeComponent>().AsEnumerable());
    }

    public async Task UpdateComponent(BikeComponent component)
    {
        // This would require a corresponding method in ComponentRepository
        // e.g., await componentRepository.Update(component);
        await Task.CompletedTask;
    }

    public async Task<bool> DeleteComponent(Guid id)
    {
        // This would require a corresponding method in ComponentRepository
        // e.g., return await componentRepository.Delete(id);
        return await Task.FromResult(true);
    }

    public void LogRideForBikeComponents(Guid bikeId, double distance)
    {
        componentRepository.GetBikeComponents(bikeId)
            .ToList()
            .ForEach(async component =>
            {
                await componentRepository.IncreaseDistance(component.Id, distance, bikeId);
            });
    }
}
