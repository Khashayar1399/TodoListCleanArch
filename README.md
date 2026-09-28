# Todo Clean Architecture (.NET 8)

A simple and robust To-Do List API built following **Clean Architecture** principles and **CQRS pattern** with MediatR.

## Architecture Layers

- **Domain:** Pure business logic and entities (`TodoItem`), free of external dependencies.
- **Application:** Orchestrates business use cases using CQRS commands/queries and defines repository interfaces.
- **Infrastructure:** Implements persistence using Entity Framework Core with SQLite.
- **Presentation (API):** ASP.NET Core Web API with Swagger documentation.

## Tech Stack
- .NET 8
- MediatR (CQRS)
- Entity Framework Core (SQLite)
- Swagger / OpenAPI

## How to Run
1. Clone the repository.
2. Open `TodoCleanArch.sln` in Visual Studio.
3. Set `TodoCleanArch.Api` as Startup Project.
4. Press `F5` to run and view Swagger UI.