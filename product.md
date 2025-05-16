# Bike Wear Tracker

This product focuses on tracking the component wear of your bicycles to prolong the life and easy maintenance

---
## Implementation Plan
Using [Marten](https://martendb.io/introduction.html) for Postgres as Document storage and Event Sourcing Events

### Entities

#### Bike
```json
{
    "id": "guid",
    "brand": "string",
    "model": "string",
    "year": "number",
    "bikeType": {
        "enum": [
            "road",
            "mountain",
            "gravel",
            "city",
            "ebike",
            "hybrid"
        ]
    }
}
```

#### Chain
```json
{
    "id": "guid",
    "bikeId": "guid",
    "brand": "string",
    "model": "string",
    "purchaseDate": "date",
    "distance": "number"
}
```

#### Tires
```json
{
    "id": "guid",
    "bikeId": "guid",
    "brand": "string",
    "model": "string",
    "purchaseDate": "date",
    "distance": "number",
    "position": {
        "enum": [
            "front",
            "rear"
        ]
    }
}
```
### Events

#### BikeRegistered
```json
{
    "eventId": "guid",
    "bikeId": "guid",
    "brand": "string",
    "model": "string",
    "year": "number",
    "bikeType": "string", // "road", "mountain", etc.
    "registeredAt": "timestamp"
}
```

#### ComponentAdded
```json
{
    "eventId": "guid",
    "bikeId": "guid",
    "componentId": "guid",
    "componentType": "string", // "Chain", "Tires"
    "brand": "string",
    "model": "string",
    "purchaseDate": "date",
    "position": "string", // Optional: "front", "rear" for tires
    "addedAt": "timestamp"
}
```

#### RideLogged
```json
{
    "eventId": "guid",
    "bikeId": "guid",
    "distance": "number",
    "rideDate": "date",
    "loggedAt": "timestamp"
}
```

#### ComponentDistanceIncreased
```json
{
    "eventId": "guid",
    "componentId": "guid",
    "distanceAdded": "number",
    "rideId": "guid", // Reference to RideLogged event
    "updatedAt": "timestamp"
}
```

#### MaintenanceLogged
```json
{
    "eventId": "guid",
    "bikeId": "guid",
    "componentId": "guid", // Optional
    "activityType": "string", // e.g., "Cleaned", "Replaced", "Adjusted"
    "description": "string",
    "datePerformed": "date",
    "notes": "string", // Optional
    "loggedAt": "timestamp"
}
```

#### ComponentReplaced
```json
{
    "eventId": "guid",
    "bikeId": "guid",
    "oldComponentId": "guid",
    "newComponentId": "guid",
    "componentType": "string", // "Chain", "Tires"
    "replacementDate": "timestamp"
}
```

---

## Example Use Case: Tracking Chain Wear and Replacement

This example demonstrates how the system uses event sourcing to track a bike's chain usage and replacement.

### Scenario

1. **Register a new bike**
2. **Add a new chain to the bike**
3. **Log several rides**
4. **Increase the chain's distance after each ride**
5. **Replace the chain after it wears out**

### Event Stream

```json
[
    // 1. Register bike
    {
        "eventType": "BikeRegistered",
        "eventId": "1",
        "bikeId": "bike-123",
        "brand": "Trek",
        "model": "Domane",
        "year": 2022,
        "bikeType": "road",
        "registeredAt": "2024-06-01T10:00:00Z"
    },
    // 2. Add chain (Shimano Ultegra)
    {
        "eventType": "ComponentAdded",
        "eventId": "2",
        "bikeId": "bike-123",
        "componentId": "chain-abc",
        "componentType": "Chain",
        "brand": "Shimano",
        "model": "Ultegra",
        "purchaseDate": "2024-06-01",
        "addedAt": "2024-06-01T10:05:00Z"
    },
    // Chain distance: 0
    // 3. Log ride #1 (50km)
    {
        "eventType": "RideLogged",
        "eventId": "3",
        "bikeId": "bike-123",
        "distance": 50,
        "rideDate": "2024-06-02",
        "loggedAt": "2024-06-02T18:00:00Z"
    },
    // 4. Increase chain distance by 50km (after ride #1)
    {
        "eventType": "ComponentDistanceIncreased",
        "eventId": "4",
        "componentId": "chain-abc",
        "distanceAdded": 50,
        "rideId": "3",
        "updatedAt": "2024-06-02T18:01:00Z"
    },
    // Chain distance: 50
    // 5. Log ride #2 (60km)
    {
        "eventType": "RideLogged",
        "eventId": "5",
        "bikeId": "bike-123",
        "distance": 60,
        "rideDate": "2024-06-05",
        "loggedAt": "2024-06-05T19:00:00Z"
    },
    // 6. Increase chain distance by 60km (after ride #2)
    {
        "eventType": "ComponentDistanceIncreased",
        "eventId": "6",
        "componentId": "chain-abc",
        "distanceAdded": 60,
        "rideId": "5",
        "updatedAt": "2024-06-05T19:01:00Z"
    },
    // Chain distance: 110
    // 7. Replace chain (Shimano Ultegra -> SRAM Force)
    {
        "eventType": "ComponentReplaced",
        "eventId": "7",
        "bikeId": "bike-123",
        "oldComponentId": "chain-abc",
        "newComponentId": "chain-def",
        "componentType": "Chain",
        "replacementDate": "2024-07-01T10:00:00Z"
    },
    // Chain-abc is retired at 110km, chain-def starts at 0
    // 8. Add new chain (SRAM Force)
    {
        "eventType": "ComponentAdded",
        "eventId": "8",
        "bikeId": "bike-123",
        "componentId": "chain-def",
        "componentType": "Chain",
        "brand": "SRAM",
        "model": "Force",
        "purchaseDate": "2024-07-01",
        "addedAt": "2024-07-01T10:00:01Z"
    }
    // Chain-def distance: 0
]
```

### Explanation

- Each event records a fact that happened, not the resulting state.
- State (e.g., total chain distance, which chain is on the bike) is reconstructed by replaying events in order.
- No derived data (like `newTotalDistance` or `distanceAtReplacement`) is stored in events; these are calculated by projecting the event stream.
- This approach provides a full audit trail and enables easy debugging, analytics, and state reconstruction at any point in time.

---

## How to Create a New .NET WebAPI Project (Custom Name, Solution in Root, Project in src)

1. **Create the solution in the root folder:**
   ```sh
   dotnet new sln -n YourSolutionName
   ```

2. **Create the WebAPI project inside the `src` folder:**
   ```sh
   dotnet new webapi -n YourProjectName -o src/YourProjectName
   ```

3. **Add the project to the solution:**
   ```sh
   dotnet sln YourSolutionName.sln add src/YourProjectName/YourProjectName.csproj
   ```

- Replace `YourSolutionName` and `YourProjectName` with your desired names.
- This will result in:
  ```
  /YourSolutionName.sln
  /src/YourProjectName/YourProjectName.csproj
  ```


