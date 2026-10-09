# Squirrel.Serialization

Message serialization registration for MemoryPack-based application messaging.

## Install

```bash
dotnet add package Squirrel.Serialization
```

## Register

```csharp
var builder = WebApplication.CreateBuilder(args);
builder.Services.AddMemoryPackSerialization();
```

Use the serialization contracts from `Squirrel.Abstractions` in messages shared between services. Add generated or explicitly registered MemoryPack formatters for custom types.
