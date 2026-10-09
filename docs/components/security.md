# Squirrel.Security

JWT and API-key authentication helpers for ASP.NET Core services.

## Install

```bash
dotnet add package Squirrel.Security
```

## JWT registration

```csharp
var builder = WebApplication.CreateBuilder(args);
builder.AddCustomJwtAuthentication(builder.Configuration);
```

Configure the JWT options through the application's configuration and protect endpoints with standard ASP.NET Core authorization policies.

## API keys

Use the API-key registration extension when an internal or integration endpoint needs API-key authentication. Keep keys in a secret store and rotate them without changing application code.
