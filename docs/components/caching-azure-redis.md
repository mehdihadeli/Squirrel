# Squirrel.Caching.AzureRedis

Azure Redis connection and cache integration for Squirrel caching.

## Install

```bash
dotnet add package Squirrel.Caching.AzureRedis
```

## Use it with

Install this package together with `Squirrel.Caching`. Configure the Redis connection using the options expected by the package, then call the shared caching registration from `Squirrel.Caching`.

```csharp
var builder = WebApplication.CreateBuilder(args);
builder.AddCustomCaching();
```

Keep provider-specific configuration in the service's configuration system so local development can use a local Redis instance and deployed environments can use Azure Managed Redis.
