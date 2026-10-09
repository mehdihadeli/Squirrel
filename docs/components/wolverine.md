# Squirrel.Integration.Wolverine

Wolverine and RabbitMQ integration for commands, events, message persistence, and durable messaging.

## Install

```bash
dotnet add package Squirrel.Integration.Wolverine
```

## Register

```csharp
var builder = WebApplication.CreateBuilder(args);
builder.AddWolverineEventBus(
    assemblies: [typeof(OrderPlacedHandler).Assembly],
    durabilityConnectionStringName: "messaging");
```

The package discovers handlers from the supplied assemblies, configures RabbitMQ, provisions message topology, and connects Squirrel event-bus abstractions to Wolverine. Configure the RabbitMQ connection through `ConnectionStrings:rabbitmq` or the package options.

Use `PublishToPrimaryExchange<TMessage>` and `ListenToPrimaryExchange<TMessage>` when custom topology is needed.
