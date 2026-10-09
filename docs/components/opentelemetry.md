# Squirrel.OpenTelemetry

OpenTelemetry tracing, metrics, logging, and common instrumentation setup.

## Install

```bash
dotnet add package Squirrel.OpenTelemetry
```

## Register

```csharp
var builder = WebApplication.CreateBuilder(args);
builder.AddDefaultOpenTelemetry();
```

Configure exporters and resource attributes through the available options callback or application configuration. Add database, messaging, and cache instrumentation packages only for dependencies used by the service.
