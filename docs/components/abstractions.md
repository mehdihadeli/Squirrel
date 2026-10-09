# Squirrel.Abstractions

Contracts shared by Squirrel applications and infrastructure adapters.

## Install

```bash
dotnet add package Squirrel.Abstractions
```

## Use it when

Use this package when an application needs Squirrel contracts without taking a dependency on concrete implementations. It contains domain entities, aggregates, commands, queries, events, message envelopes, repository contracts, paging models, serialization contracts, and web module contracts.

## Example

```csharp
public sealed record OrderPlaced(Guid OrderId) : IIntegrationEvent;

public sealed class GetOrder : IQuery<OrderDetails>
{
    public required Guid OrderId { get; init; }
}
```

Implement these contracts in application code, then add the package that provides the required infrastructure implementation.
