using System;
using backend.Domain.Entities;

namespace backend.Application.Service;

public class RideService(BikeService bikeService, ComponentService componentService)
{
    public async Task AddRide(Ride ride)
    {
        await bikeService.AddRideToBike(ride.BikeId, ride.Distance, ride.RideDate);
        componentService.LogRideForBikeComponentsAsync(ride.BikeId, ride.Distance);
    }

}
