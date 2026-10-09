# Squirrel.Persistence.Marten

Marten document and event-sourcing integration for PostgreSQL.

## Install

```bash
dotnet add package Squirrel.Persistence.Marten
```

## Register

```csharp
var builder = WebApplication.CreateBuilder(args);
builder.Services.AddMartenDb(
    builder.Configuration,
    scanAssemblies: [typeof(OrderAggregate).Assembly]);
```

The integration configures event sourcing, lightweight sessions, schema changes at startup, an asynchronous subscription daemon, and Squirrel event consumers. Configure the connection string and read/write schema options through `MartenOptions`.
