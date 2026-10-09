# Squirrel.Persistence.Mongo

MongoDB context, repository, unit-of-work, health-check, and tracing integration.

## Install

```bash
dotnet add package Squirrel.Persistence.Mongo
```

## Register

```csharp
var builder = WebApplication.CreateBuilder(args);
builder.AddMongoDbContext<AppMongoContext>("catalogdb");
```

The context must implement `MongoDbContext` and `IMongoDbContext`. The package reads `ConnectionStrings:catalogdb` when supplied, otherwise it uses `MongoOptions.ConnectionString`, registers Mongo repositories and unit of work, and enables tracing and health checks unless disabled in options.
