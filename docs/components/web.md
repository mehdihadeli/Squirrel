# Squirrel.Web

ASP.NET Core web conventions for CORS, compression, versioning, problem details, rate limiting, and minimal APIs.

## Install

```bash
dotnet add package Squirrel.Web
```

## Register common web services

```csharp
var builder = WebApplication.CreateBuilder(args);
builder.AddDefaultCors();
builder.AddCompression();
builder.AddCustomVersioning();
builder.AddCustomRateLimit();
builder.AddCustomProblemDetails(new[] { typeof(Program).Assembly });

var app = builder.Build();
app.UseDefaultCors();
```

Use `AddMinimalEndpoints` and `MapMinimalEndpoints` when the service follows the Squirrel minimal endpoint/module conventions. The package also provides typed problem results and command/query endpoint mapping helpers.
