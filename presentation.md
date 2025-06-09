---
marp: true
theme: rose-pine-moon
paginate: true
---

# Bike Wear Tracker  

Event Sourcing and CQRS in .NET

---

## Introduction & Motivation

- **Event Sourcing**: records all state changes as immutable events
- **Bike Wear Tracker**: helps cyclists monitor wear and maintenance

---

## Problem Statement

- Traditional CRUD systems struggle with:
  - **Auditability**: Hard to reconstruct history
  - **Evolvability**: Schema changes are risky
  - **Reliability**: Data loss/corruption is hard to detect

---

## Solution Overview

- **Event Sourcing**: Every change is an immutable event
- **CQRS**: Separate write (commands) and read (queries) models
- **Clean Architecture**: Clear separation of concerns
- **Tech Stack**:
  - .NET 9.0, Marten (PostgreSQL), Wolverine, REST API, Docker

---

## Architecture Diagram

![bg right](./System-Diagram.png)

---

## Key Features

- **Register Bikes**: Track multiple bikes per user
- **Add/Replace Components**: Full lifecycle tracking
- **Log Rides**: Updates mileage for bikes/components
- **Audit Trail**: Every change is an event
- **REST API**: For integration and automation

---

## Example Workflow

### Tracking Chain Wear

1. Register Bike  
2. Add Chain  
3. Log Rides  
4. Replace Chain

---

## Example Event Stream

```json
[
  { "eventType": "BikeRegisteredEvent", ... },
  { "eventType": "ComponentAddedEvent", ... },
  { "eventType": "RideLoggedEvent", ... },
  { "eventType": "ComponentReplacedEvent", ... }
]
```

---

## Event Sourcing with Marten

```csharp
// Start a new event stream for a bike
var streamId = session.Events.StartStream<Bike>(bikeId, new BikeRegisteredEvent(...)).Id;
await session.SaveChangesAsync();

// Append a new event (e.g., RideLoggedEvent)
session.Events.Append(bikeId, new RideLoggedEvent(...));
await session.SaveChangesAsync();
```

*Events are stored in PostgreSQL and can be replayed to reconstruct state.*

---

## CQRS with Wolverine

```csharp
await bus.InvokeAsync<Guid?>(command);

// Command handler example
public class RegisterBikeHandler(BikeRepository repo)
{
    public async Task<Guid> Handle(RegisterBikeCommand command)
    {
        var evt = new BikeRegisteredEvent(...);
        return await repo.Add(evt);
    }
}
```

*Wolverine registers and dispatches commands and queries to handlers, decoupling write and read logic.*

---

## Aggregation & Projections

```csharp
public async Task<Bike?> Get(Guid id)
{
    using var session = martenContext.GetQuerySession();
    var bikeStream = await session.Events.AggregateStreamAsync<Bike>(id);
    return bikeStream;
}
```

```csharp
public class BikeAggregation : SingleStreamProjection<Bike>
{
    public Bike Create(BikeRegisteredEvent e) => new Bike { ... };
    public void Apply(RideLoggedEvent e, Bike bike) => bike.TotalDistance += e.Distance;
    public void Apply(ComponentAddedEvent e, Bike bike) => bike.Components.Add(...);
}
```

*Marten projections rebuild the current state from the event stream.*

---

## Benefits

- **Full Auditability**: Reconstruct any state at any time
- **Easy Evolution**: Add new events/commands safely
- **Reliability**: Immutable event log
- **Performance**: Optimized read models

---

## Benefits for Microservices

- **Decoupled Services**: Event streams enable loose coupling between services
- **Scalable Communication**: Services can subscribe to events for integration
- **Resilience**: Services can rebuild state from events after failures
- **Autonomous Evolution**: Each service can evolve its own event schema
- **Audit & Traceability**: End-to-end traceability across service boundaries
