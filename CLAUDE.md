# CLAUDE.md

This file provides guidance to Claude Code (claude.ai/code) when working with code in this repository.

## Project Overview

KiiBlog is an ASP.NET Core 8.0 Blazor Server application following Clean Architecture principles with CQRS pattern using MediatR. The solution is organized into distinct layers with clear separation of concerns.

## Solution Structure

The solution follows a layered architecture pattern:

- **KiiBlog.Domain** - Core domain entities (PLAYER, FLEX, FLEX_ITEM) with no external dependencies
- **KillBlog.DTO** - Data Transfer Objects for queries/commands (note the typo in project name)
- **KiiBlog.Application** - Business logic layer with MediatR handlers, repository interfaces, and UnitOfWork pattern
- **KiiBlog.Infrastructure** - Data access implementation using Entity Framework Core 9.0 with SQL Server
- **KiiBlog.WebUI.Server** - Blazor Server presentation layer using Syncfusion components

## Key Architectural Patterns

### Repository Pattern & Unit of Work

The codebase uses a generic repository pattern with Unit of Work for data access:

- **IUnitOfWork** (`KiiBlog.Application/UnitOfWork/IUnitOfWork.cs`) - Coordinates repositories and transactions
- **UnitOfWork** (`KiiBlog.Infrastructure/Persistence/UnitOfWork.cs`) - Implementation with transaction support
- **IRepositories<T>** - Generic repository interface with query methods (GetAsync, GetManyAsync, GetQueryable)
- Repository instances are lazily initialized in UnitOfWork (e.g., `_player ??= new Repositories<PLAYER>(_context)`)

Transaction management methods:
- `BeginTransactionAsync()` - Start a transaction
- `CommitTransactionAsync()` - Commit with automatic rollback on failure
- `RollbackTransactionAsync()` - Manual rollback
- `ExecuteInTransactionAsync()` - Execute operations within a transaction scope (supports nested transactions)

### CQRS with MediatR

Queries and commands are handled via MediatR:
- Queries are defined as `IRequest<TResult>` (e.g., `GetAllPlayersQuery`)
- Handlers implement `IRequestHandler<TQuery, TResult>`
- Handlers inject `IUnitOfWork` to access repositories
- Example: `GetAllPlayersQueryHandler` uses `_unitOfWork.Player.GetManyAsync()`

### Entity Framework Core Configuration

- **ApplicationDbContext** - Main DbContext with DbSets for PLAYER, FLEX, FLEX_ITEM
- Entity configurations auto-applied via `modelBuilder.ApplyConfigurationsFromAssembly()`
- Individual entity configurations in `KiiBlog.Infrastructure/Configuration/` folder
- Navigation properties: FLEX has one-to-many with FLEX_ITEM

### Dependency Injection

Each layer has a `DependencyInjection.cs` static class with extension methods:
- `AddApplication()` - Registers MediatR from Application assembly
- `AddInfrastructure(IConfiguration)` - Registers DbContext with SQL Server and UnitOfWork

Registered in `Program.cs`:
```csharp
builder.Services.AddInfrastructure(builder.Configuration);
builder.Services.AddApplication();
```

## Common Commands

### Build and Run
```bash
# Build entire solution
dotnet build

# Run the web application
dotnet run --project KiiBlog.WebUI.Server

# Build specific project
dotnet build KiiBlog.Infrastructure
```

### Database Migrations
```bash
# Add new migration (run from solution root)
dotnet ef migrations add <MigrationName> --project KiiBlog.Infrastructure --startup-project KiiBlog.WebUI.Server

# Update database
dotnet ef database update --project KiiBlog.Infrastructure --startup-project KiiBlog.WebUI.Server

# Remove last migration
dotnet ef migrations remove --project KiiBlog.Infrastructure --startup-project KiiBlog.WebUI.Server

# Generate SQL script
dotnet ef migrations script --project KiiBlog.Infrastructure --startup-project KiiBlog.WebUI.Server
```

### Testing
```bash
# Run all tests (if test projects exist)
dotnet test

# Run specific test project
dotnet test <TestProjectName>
```

## Database Configuration

Connection string in `appsettings.json`:
- Server: `localhost\SQLEXPRESS`
- Database: `KiiBlog`
- Authentication: Windows Authentication (Trusted_Connection)
- Certificate validation: Disabled (TrustServerCertificate=True)

## Domain Entities Pattern

All entities follow a consistent pattern with:
- Primary key: `{ENTITY}_ID` (int)
- Audit fields: `CREATED_BY_ID`, `CREATED_DATE`, `LAST_UPDATE_ID`, `LAST_UPDATE_DATE`
- Soft delete: `IS_DELETE` (bool)
- Active flag: `IS_ACTIVE` (bool)
- Row version: `ROW_UN` (Guid)
- UPPERCASE naming convention for all properties

## Creating New Features

When adding new entities or features:

1. **Domain Layer**: Create entity in `KiiBlog.Domain/Entities/`
2. **Infrastructure Layer**:
   - Add DbSet to `ApplicationDbContext`
   - Create entity configuration in `Configuration/` folder
   - Add repository property to `IUnitOfWork` and `UnitOfWork` implementation
3. **Application Layer**:
   - Create DTOs in `KillBlog.DTO/` (note the project name)
   - Create query/command in appropriate folder
   - Create handler implementing `IRequestHandler`
4. **Generate Migration**: Run `dotnet ef migrations add <Name>`
5. **Presentation Layer**: Create Blazor components in `Components/Pages/`

## Notes

- The DTO project has a typo: "KillBlog" instead of "KiiBlog"
- Blazor Server uses Syncfusion components (commercial license may be required)
- All database queries should use repository methods with optional eager loading via `includes` parameter
- Use `asNoTracking: true` for read-only queries to improve performance
- Complex queries should leverage `GetQueryable()` for full LINQ support
