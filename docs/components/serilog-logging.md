# Squirrel.SerilogLogging

Serilog setup, request enrichment, baggage enrichment, and structured logging conventions.

## Install

```bash
dotnet add package Squirrel.SerilogLogging
```

## Register

```csharp
var builder = WebApplication.CreateBuilder(args);
builder.AddCustomSerilog();
```

Use `WithBaggage` when configuring the logger to copy distributed tracing baggage into structured log properties. Configure sinks and minimum levels through the normal Serilog configuration system.
