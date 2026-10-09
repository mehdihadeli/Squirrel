# Squirrel.Core

Application and domain building blocks built on the abstractions package.

## Install

```bash
dotnet add package Squirrel.Core
```

## Register

```csharp
var builder = WebApplication.CreateBuilder(args);
builder.AddCoreServices();
```

`AddCoreServices` registers the command bus, query bus, domain event services, diagnostics services, serializers, and common application infrastructure used by the other packages.

## Use it when

Use this package as the foundation for a service that needs commands, queries, domain events, paging, common exceptions, validation helpers, and shared dependency-registration conventions.
