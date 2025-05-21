# Bike Wear Tracker

This product helps you track the wear and maintenance of your bicycle components for better longevity and easier upkeep.

---

## Implementation Plan

Using [Marten](https://martendb.io/introduction.html) for PostgreSQL as the event store and document storage, following event sourcing, CQRS, and Clean Architecture patterns. Wolverine is used as the mediator for commands and queries.

### Entities

#### Bike

```json
{
  "id": "guid",
  "brand": "string",
  "model": "string",
  "serialNumber": "string",
  "year": "number",
  "bikeType": "string",
  "totalDistance": "number",
  "components": [
    {
      "componentId": "guid",
      "bikeId": "guid",
      "componentType": "string",
      "brand": "string",
      "model": "string",
      "purchaseDate": "date",
      "position": "string",
      "addedAt": "date",
      "mileage": "number"
    }
  ]
}
```

#### BikeComponent

```json
{
  "componentId": "guid",
  "bikeId": "guid",
  "componentType": "string",
  "brand": "string",
  "model": "string",
  "purchaseDate": "date",
  "position": "string",
  "addedAt": "date",
  "mileage": "number"
}
```

#### Ride

```json
{
  "id": "guid",
  "bikeId": "guid",
  "distance": "number",
  "rideDate": "date",
  "addedAt": "date"
}
```

---

## Events

Events represent facts that have occurred in the system and are the source of truth for all state.

### BikeRegisteredEvent

```json
{
  "id": "guid",
  "brand": "string",
  "model": "string",
  "serialNumber": "string",
  "year": "number",
  "bikeType": "string"
}
```

### ComponentAddedEvent

```json
{
  "componentId": "guid",
  "bikeId": "guid",
  "componentType": "string",
  "brand": "string",
  "model": "string",
  "purchaseDate": "date",
  "position": "string",
  "addedAt": "date"
}
```

### ComponentReplacedEvent

```json
{
  "bikeId": "guid",
  "oldComponentId": "guid",
  "newComponentId": "guid",
  "componentType": "string",
  "brand": "string",
  "model": "string",
  "purchaseDate": "date",
  "addedAt": "date",
  "position": "string"
}
```

### RideLoggedEvent

```json
{
  "bikeId": "guid",
  "rideId": "guid",
  "distance": "number",
  "rideDate": "date",
  "loggedAt": "date"
}
```

---

## Commands

Commands represent user intentions and actions in the system.

### RegisterBikeCommand

```json
{
  "brand": "string",
  "model": "string",
  "serialNumber": "string",
  "year": "number",
  "bikeType": "string"
}
```

### AddComponentCommand

```json
{
  "bikeId": "guid",
  "componentType": "string",
  "brand": "string",
  "model": "string",
  "purchaseDate": "date",
  "position": "string"
}
```

### ReplaceComponentCommand

```json
{
  "bikeId": "guid",
  "oldComponentId": "guid",
  "componentType": "string",
  "brand": "string",
  "model": "string",
  "purchaseDate": "date",
  "position": "string"
}
```

### LogRideCommand

```json
{
  "bikeId": "guid",
  "distance": "number",
  "rideDate": "date"
}
```

---

## Queries

Queries represent read operations in the system.

### GetBikeQuery

```json
{
  "bikeId": "guid"
}
```

---

## Example Use Case: Tracking Chain Wear and Replacement

This example demonstrates how the system uses commands and events to track a bike's chain usage and replacement.

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
    "brand": "Trek",
    "model": "Domane",
    "serialNumber": "SN-001",
    "year": 2022,
    "bikeType": "road"
  },
  {
    "bikeId": "bike-123",
    "componentType": "Chain",
    "brand": "Shimano",
    "model": "Ultegra",
    "purchaseDate": "2024-06-01",
    "position": null
  },
  {
    "bikeId": "bike-123",
    "distance": 50,
    "rideDate": "2024-06-02"
  },
  {
    "bikeId": "bike-123",
    "distance": 60,
    "rideDate": "2024-06-05"
  },
  {
    "bikeId": "bike-123",
    "oldComponentId": "chain-abc",
    "componentType": "Chain",
    "brand": "Shimano",
    "model": "Ultegra",
    "purchaseDate": "2024-07-01",
    "position": null
  }
]
```