# Event Sourcing for Bike Wear Tracking: A Synopsis

## 1. Introduction / Motivation
Event sourcing is a powerful architectural pattern that addresses critical challenges in modern software systems, such as reliability, evolvability, and auditability. This project explores the application of event sourcing to develop a "Bike Wear Tracker" system. The system aims to help cyclists track the wear and maintenance of their bicycle components, thereby enhancing component longevity and simplifying upkeep. By leveraging event sourcing, the project demonstrates how to build a robust application that maintains a transparent history of all changes, adapts to evolving requirements, and ensures data integrity.

## 2. Problem Statement
Traditional data management approaches often struggle to simultaneously meet the demands for reliability, scalability, evolvability, and auditability. This project addresses the challenge of designing a system for tracking bicycle component wear that is:
- **Reliable**: Ensuring data consistency and resilience to failures.
- **Evolvable**: Allowing the system to adapt to new features and changing requirements without extensive refactoring.
- **Auditable**: Maintaining a complete and immutable history of all operations for tracking and diagnostic purposes.
- **Scalable**: Capable of handling a growing number of users, bikes, and components.

The "Bike Wear Tracker" will serve as a practical case study to demonstrate how event sourcing, combined with CQRS (Command Query Responsibility Segregation), can effectively address these challenges.

## 3. Methodology

### 3.1 Product Overview

The **Bike Wear Tracker** is a web-based application designed for cyclists to manage and monitor the wear, maintenance, and replacement of their bicycle components. The system enables users to:

- Register and manage multiple bikes.
- Add, replace, and track the lifecycle of individual components (e.g., chains, tires, brakes).
- Log rides, automatically updating mileage for bikes and their components.
- View detailed histories and audit trails for all bikes and components.
- Access a REST API for integration with other tools or mobile apps.

The application is built using .NET 9.0 and follows Clean Architecture principles, ensuring a clear separation between domain logic, application services, infrastructure, and presentation layers. The backend leverages Marten for event storage and projections, Wolverine for command/query mediation, and PostgreSQL as the underlying database.

### 3.2 Architectural Patterns and Technology Stack

- **Event Sourcing**: All state changes are captured as immutable events. The current state of any entity (e.g., a bike or component) is derived by replaying its event stream.
- **CQRS (Command Query Responsibility Segregation)**: The system separates write operations (commands) from read operations (queries), allowing each to be optimized independently.
- **Clean Architecture**: The codebase is organized into layers:
  - **Domain**: Core business logic, aggregates, and domain events.
  - **Application**: Command/query handlers, use cases, and DTOs.
  - **Infrastructure**: Data access, event store (Marten), and external integrations.
  - **Presentation**: REST API controllers.
- **Wolverine**: Used as the mediator for dispatching commands and queries, supporting asynchronous and decoupled processing.
- **Marten**: Provides event sourcing and document storage capabilities on top of PostgreSQL, including projections for efficient querying.
- **PostgreSQL**: Serves as the persistent store for events and read models.
- **REST API**: Exposes endpoints for all core operations, enabling integration with web and mobile clients.
- **Swagger/OpenAPI**: Used for documenting and exploring the REST API, ensuring clear contracts and discoverability for consumers.
- **Docker**: The application and its dependencies (including PostgreSQL) are containerized for consistent deployment and easy scalability.

### 3.3 Service Scoping & Bounded Context

The system is scoped around the **Bike Wear Tracking** bounded context, which encapsulates all logic related to bikes, components, rides, and maintenance. All business rules, events, and data models are defined within this context, ensuring clear boundaries and minimizing coupling with external systems. This approach supports future extensibility, such as integrating with other cycling or fitness services, by exposing well-defined APIs and events.

### 3.4 Service Design & Architecture

The service is designed as a modular, single-responsibility application following Clean Architecture. The domain layer contains aggregates (e.g., `Bike`), value objects, and domain events. The application layer orchestrates use cases via command and query handlers, leveraging Wolverine for mediation. The infrastructure layer handles persistence (Marten/PostgreSQL), messaging, and external integrations. The presentation layer exposes a REST API, documented with Swagger, for all client interactions.

### 3.5 Communication: REST and Messaging

- **REST API**: All client interactions (web, mobile, integrations) occur via a RESTful API, supporting standard HTTP verbs and resource-oriented URIs. The API is self-documented using Swagger/OpenAPI, allowing for easy exploration and testing.
- **Messaging**: Internally, Wolverine is used for command and event dispatching, supporting asynchronous processing and decoupling between components. This enables future extension to distributed messaging or integration with external event buses if required.

### 3.6 API Documentation

The REST API is fully documented using **Scalar/OpenAPI**. This provides:
- Interactive documentation for developers and integrators.
- Clear contracts for all endpoints, request/response models, and error handling.

### 3.7 Deployment using Docker

The application is containerized using **Docker**, enabling:
- Consistent deployment across environments (development, staging, production).
- Easy orchestration of dependencies, such as PostgreSQL, via Docker Compose.
- Scalability and portability for cloud or on-premises hosting.

A typical deployment includes:
- The .NET application container.
- A PostgreSQL container for event and read model storage.

## 4. Analysis & Results

### 4.1 Implemented Functionalities

The project has implemented basic functionalities in order to test the event sourcing pattern. Key functionalities developed include:

- **Bike Management**:
    - Registering a new bike (`RegisterBikeCommand` -> `BikeRegisteredEvent`).
    - Retrieving bike details (queries on projected `Bike` read models).
- **Component Management**:
    - Adding components to a bike (`AddComponentCommand` -> `ComponentAddedEvent`).
    - Replacing components (`ReplaceComponentCommand` -> `ComponentReplacedEvent`).
- **Ride Logging**:
    - Logging rides for a bike (`LogRideCommand` -> `RideLoggedEvent`).
    - Updating bike and component mileage based on logged rides (event handlers update projections).

### 4.2 Architectural Strengths

The system's architecture demonstrates several key strengths:

1. **Ease of Implementing New Features and Modifying Existing Ones**  
    Leveraging CQRS and Clean Architecture, the system separates command (write) and query (read) responsibilities. Commands and events are explicitly defined, with business logic encapsulated in handlers and aggregates. This modularity enables developers to introduce new features—such as additional commands, events, or projections—without affecting unrelated components. For example, adding a new maintenance action involves creating a new command and event, with minimal changes to existing code, thanks to the decoupled structure and Wolverine's mediation.

2. **Performance of Command Processing and Query Execution**  
    Command processing is efficient, as commands are handled asynchronously and events are appended directly to the Marten event store. Marten's inline projections update read models in real-time, supporting fast and responsive queries. PostgreSQL provides a scalable and reliable foundation for both event storage and read model queries. The CQRS pattern allows independent optimization of write and read paths, further enhancing performance.

3. **Completeness and Accessibility of the Audit Trail**  
    Every state change is recorded as an immutable event in the event store, ensuring a comprehensive and tamper-proof audit trail. Marten enables straightforward reconstruction of entity histories and supports generating audit reports. The audit trail is easily accessible, either by querying event streams directly or by building projections tailored for auditing and reporting.

4. **Overall Reliability and Maintainability**  
    The event-driven, transactional architecture ensures reliability, with all changes persisted atomically. Marten and PostgreSQL provide strong consistency and resilience to failures. Maintainability is supported by the layered structure (Domain, Application, Infrastructure, Presentation), clear contracts (commands, events), and dependency injection. Business logic is isolated from infrastructure, making the system testable and easier to evolve over time.

## 5. Conclusion

This project demonstrates how event sourcing, combined with CQRS, Clean Architecture, and modern .NET technologies such as Marten and Wolverine, can be effectively applied to build a robust, auditable, and maintainable application for tracking bike wear and maintenance. The system is scoped around a clear bounded context, designed for modularity, and exposes a well-documented REST API using Swagger. Communication is handled via REST and internal messaging, supporting extensibility and decoupling. Deployment is streamlined with Docker, ensuring consistency and scalability. The result is a system that meets the requirements for reliability, evolvability, auditability, and scalability, and serves as a practical reference for event-sourced solutions in real-world domains.
