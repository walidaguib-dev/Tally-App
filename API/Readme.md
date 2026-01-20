# API Layer (Presentation)

## Purpose
The API layer exposes the application to the outside world via **HTTP endpoints**.  
It translates requests into Application services and returns responses.

## Contents
- **Controllers/** → Handle HTTP requests (TallySheetController, AuthController).
- **Filters/** → Validation, exception handling.
- **Program.cs** → Dependency injection, middleware, authentication setup.

## Rules
- Depends on Application (calls services).
- Should not contain business logic.
- Responsible only for request/response handling.