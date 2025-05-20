using backend.Infrastructure.Repository;
using backend.Domain.Entities;

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

    public async Task IncreaseComponentDistance(Guid componentId, double distance, Guid rideId)
    {
        await componentRepository.IncreaseDistance(componentId, distance, rideId);
    }

    public async Task LogRideForBikeComponentsAsync(Guid bikeId, double distance)
    {
        var tasks = componentRepository.GetBikeComponents(bikeId)
            .Select(component => IncreaseComponentDistance(component.Id, distance, bikeId));
        await Task.WhenAll(tasks);
    }
}
