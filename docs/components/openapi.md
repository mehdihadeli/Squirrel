# Squirrel.OpenApi

OpenAPI, Swagger, API versioning, and AsyncAPI registration helpers.

## Install

```bash
dotnet add package Squirrel.OpenApi
```

## Swagger registration

```csharp
var builder = WebApplication.CreateBuilder(args);
builder.AddSwaggerOpenApi(typeof(Program).Assembly);

var app = builder.Build();
app.UseSwaggerOpenApi();
```

For ASP.NET native OpenAPI use `AddAspnetOpenApi` and `UseAspnetOpenApi`. For event-driven contracts use `AddAsyncApi` with the message types that should be included.
