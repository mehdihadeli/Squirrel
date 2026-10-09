# Squirrel.Persistence.EventStoreDB

EventStoreDB client, event-sourcing, subscriptions, tracing, and health-check integration.

## Install

```bash
dotnet add package Squirrel.Persistence.EventStoreDB
```

## Register

```csharp
var builder = WebApplication.CreateBuilder(args);
builder.AddEventStoreDb("event-store");
builder.AddEventStoreDbSubscriptionToAll();
```

The package reads an Aspire or configuration connection string and falls back to `EventStoreDbOptions.GrpcConnectionString`. Use the subscription registration when the service needs to consume every stream event and persist subscription checkpoints.
