using backend.Application.Queries;
using backend.Domain.Entities;
using backend.Infrastructure.Repository;

namespace backend.Application.Handlers;

public class GetBikeHandler(BikeRepository bikeRepository)
{
    public async Task<Bike?> Handle(GetBikeQuery query)
    {
        var bike = await bikeRepository.Get(query.BikeId);
        return bike;
    }
}