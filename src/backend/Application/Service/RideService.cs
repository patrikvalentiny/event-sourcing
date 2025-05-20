using System;
using backend.Domain.Entities;

namespace backend.Application.Service;

public class RideService(BikeService bikeService, ComponentService componentService)
{
    public async Task AddRide(Ride ride)
    {
        var bike = await bikeService.GetBike(ride.BikeId) ?? throw new Exception("Bike not found");
        await bikeService.AddRideToBike(ride.BikeId, ride.Distance, ride.RideDate);

        // Add components to the ride
        componentService.LogRideForBikeComponents(ride.BikeId, ride.Distance);
    }

}
