# Application Layer

## Purpose
The Application layer defines **workflows and orchestration** of domain logic.  
It coordinates entities and repositories to achieve business goals.

## Contents
- **DTOs/** → Data transfer objects for input/output (TallySheetDto).
- **Services/** → Application services (TallySheetService, AuthService).
- **Policies/** → Authorization rules.
- **UseCases/** (optional) → Specific workflows (CreateTallySheet, AddClientMerchandise).

## Rules
- Depends only on Domain.
- No direct database or framework code.
- Contains orchestration logic, not persistence.