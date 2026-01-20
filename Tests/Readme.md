# Tests Project

## Purpose
The Tests project validates each layer of the architecture.  
It ensures correctness, reliability, and prevents regressions.

## Contents
- **DomainTests/** → Test entity rules and value objects.
- **ApplicationTests/** → Test services and workflows (mock repositories).
- **InfrastructureTests/** → Test repository implementations with in-memory DB.
- **ApiTests/** → Integration tests for controllers and endpoints.

## Rules
- Each layer should be tested independently.
- Use mocks/fakes for dependencies.
- Integration tests should cover authentication and protected endpoints.