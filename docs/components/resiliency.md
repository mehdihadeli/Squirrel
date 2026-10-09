# Squirrel.Resiliency

Service discovery and HTTP resilience defaults using the Microsoft resilience stack.

## Install

```bash
dotnet add package Squirrel.Resiliency
```

## Register

```csharp
var builder = WebApplication.CreateBuilder(args);
builder.AddCustomResiliency(globalHttpClientResiliency: true);
```

The registration enables service discovery and can apply standard retry, timeout, and circuit-breaker handling to HTTP clients. Configure policy options through application configuration and disable global handlers when a service needs per-client policies.
