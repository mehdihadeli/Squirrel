# Squirrel.Email

Email sender abstractions and SendGrid-backed implementation.

## Install

```bash
dotnet add package Squirrel.Email
```

## Register

```csharp
var builder = WebApplication.CreateBuilder(args);
builder.Services.AddEmailService(builder.Configuration);
```

Inject the registered email service into an application handler and keep provider credentials in configuration or a secret store. Do not place API keys in source control.

## Use it when

Use this package when email delivery should be consumed through an application abstraction rather than directly coupling handlers to SendGrid APIs.
