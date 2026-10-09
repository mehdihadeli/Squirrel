# Squirrel.Persistence.EfCore.Postgres

EF Core PostgreSQL persistence with repositories, unit of work, migrations, naming conventions, interceptors, and domain-event integration.

## Install

```bash
dotnet add package Squirrel.Persistence.EfCore.Postgres
```

## Register

```csharp
var builder = WebApplication.CreateBuilder(args);
builder.AddPostgresDbContext<AppDbContext>(
    connectionStringName: "catalogdb",
    migrationAssembly: typeof(AppDbContext).Assembly,
    assembliesToScan: typeof(OrderRepository).Assembly);
```

`AppDbContext` must implement the Squirrel EF Core facade and domain-event context contracts. The registration uses Npgsql, snake-case naming, retry-on-failure, audit/concurrency/soft-delete interceptors, and repository scanning.

For startup migrations use `AddMigration<AppDbContext>()` or the overload that supplies a database seeder.
