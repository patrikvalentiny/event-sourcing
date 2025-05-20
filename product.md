# Bike Wear Tracker

This product helps you track the wear and maintenance of your bicycle components for better longevity and easier upkeep.

---

## Implementation Plan

Using [Marten](https://martendb.io/introduction.html) for PostgreSQL as the event store and document storage, following event sourcing, CQRS, and Clean Architecture patterns.

### Entities

#### Bike

```json
{
  "id": "guid",
  "brand": "string",
  "model": "string",
  "serialNumber": "string",
  "year": "number",
  "bikeType": "string", // "road", "mountain", "gravel", "city", "ebike", "hybrid"
  "mileage":"number",
  "components": [
        {
            "id": "guid",
            "bikeId": "guid",
            "componentType": "string",
            "brand": "string",
            "model": "string",
            "purchaseDate": "date",
            "position": "string",
            "mileage": "number"
        }
    ]
}
```

#### Component

```json
{
  "id": "guid",
  "bikeId": "guid",
  "componentType": "string", // "Chain", "Tires", etc.
  "brand": "string",
  "model": "string",
  "purchaseDate": "date",
  "position": "string", // Optional: "front", "rear" for tires
  "mileage": "number"
}
```

---

## Commands

Commands represent user intentions and actions in the system.

### RegisterBike

```json
{
  "commandType": "RegisterBike",
  "bikeId": "guid",
  "brand": "string",
  "model": "string",
  "serialNumber": "string",
  "year": "number",
  "bikeType": "string"
}
```

### AddComponent

```json
{
  "commandType": "AddComponent",
  "componentId": "guid",
  "bikeId": "guid",
  "componentType": "string",
  "brand": "string",
  "model": "string",
  "purchaseDate": "date",
  "position": "string" // Optional
}
```

### LogRide

```json
{
  "commandType": "LogRide",
  "rideId": "guid",
  "bikeId": "guid",
  "distance": "number",
  "rideDate": "date"
}
```

### ReplaceComponent

```json
{
  "commandType": "ReplaceComponent",
  "oldComponentId": "guid",
  "newComponentId": "guid",
  "bikeId": "guid",
  "componentType": "string",
  "brand": "string",
  "model": "string",
  "purchaseDate": "date",
  "position": "string" // Optional
}
```

---

## Example Use Case: Tracking Chain Wear and Replacement

This example demonstrates how the system uses commands to track a bike's chain usage and replacement.

### Scenario

1. Register a new bike
2. Add a new chain to the bike
3. Log several rides
4. Increase the chain's distance after each ride
5. Replace the chain after it wears out

### Command Stream

```json
[
  {
    "commandType": "RegisterBike",
    "bikeId": "bike-123",
    "brand": "Trek",
    "model": "Domane",
    "serialNumber": "SN-001",
    "year": 2022,
    "bikeType": "road"
  },
  {
    "commandType": "AddComponent",
    "componentId": "chain-abc",
    "bikeId": "bike-123",
    "componentType": "Chain",
    "brand": "Shimano",
    "model": "Ultegra",
    "purchaseDate": "2024-06-01",
    "position": null
  },
  {
    "commandType": "LogRide",
    "rideId": "ride-1",
    "bikeId": "bike-123",
    "distance": 50,
    "rideDate": "2024-06-02"
  },
  {
    "commandType": "IncreaseComponentDistance",
    "componentId": "chain-abc",
    "distanceAdded": 50,
    "rideId": "ride-1"
  },
  {
    "commandType": "LogRide",
    "rideId": "ride-2",
    "bikeId": "bike-123",
    "distance": 60,
    "rideDate": "2024-06-05"
  },
  {
    "commandType": "IncreaseComponentDistance",
    "componentId": "chain-abc",
    "distanceAdded": 60,
    "rideId": "ride-2"
  },
  {
    "commandType": "ReplaceComponent",
    "oldComponentId": "chain-abc",
    "newComponentId": "chain-def",
    "bikeId": "bike-123",
    "componentType": "Chain",
    "replacementDate": "2024-07-01"
  },
  {
    "commandType": "AddComponent",
    "componentId": "chain-def",
    "bikeId": "bike-123",
    "componentType": "Chain",
    "brand": "SRAM",
    "model": "Force",
    "purchaseDate": "2024-07-01",
    "position": null
  }
]
```

### Explanation

- Each command represents an intention or action from the user.
- The system processes commands to produce events and update state.
- State (e.g., total chain distance, which chain is on the bike) is reconstructed by applying events resulting from commands.
- This approach provides a clear audit trail and enables easy debugging, analytics, and state reconstruction at any point in time.