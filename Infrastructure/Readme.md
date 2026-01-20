# Infrastructure Layer

## Purpose
The Infrastructure layer provides **technical implementations** for persistence, authentication, and external services.  
It connects the abstract contracts in Domain to real frameworks like EF Core, Identity, and SignalR.

## Contents
- **Persistence/** → EF Core DbContext, migrations.
- **Repositories/** → Implement domain interfaces (TallySheetRepository).
- **Services/** → JWT generator, SignalR sync, logging.

## Rules
- Depends on Domain (to implement contracts).
- Can use frameworks (EF Core, ASP.NET Identity).
- Replaceable: you can swap SQL Server for PostgreSQL without changing Domain/Application.