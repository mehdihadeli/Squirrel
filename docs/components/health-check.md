# Squirrel.HealthCheck

Default ASP.NET Core health-check registration and endpoint conventions.

## Install

```bash
dotnet add package Squirrel.HealthCheck
```

## Register

```csharp
var builder = WebApplication.CreateBuilder(args);
builder.AddDefaultHealthChecks();

var app = builder.Build();
app.MapDefaultHealthChecks();
```

Add provider-specific health-check packages separately when the service depends on databases, brokers, or external systems. Keep readiness and liveness behavior aligned with your deployment platform.
