# Domain Layer

## Purpose
The Domain layer contains the **core business logic** of the port logistics system.  
It is completely independent of frameworks, databases, and UI.  
This is the "heart" of the application.

## Contents
- **Entities/** → Core business objects (Vessel, TallySheet, Client, Merchandise, Truck, Observation).
- **ValueObjects/** → Immutable concepts (ShiftTime, ZoneName, CargoWeight).
- **Enums/** → Domain-specific enumerations (ObservationType).
- **Interfaces/** → Contracts for repositories/services (ITallySheetRepository, IVesselRepository).
- **Exceptions/** → Custom domain exceptions (InvalidShiftTimeException).
- **Events/** → Domain events (TallySheetCreatedEvent).

## Rules
- No dependencies on other projects.
- Only pure C# classes and business rules.
- Equality for Value Objects is by value, not identity.