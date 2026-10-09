# Squirrel

Reusable infrastructure building blocks for .NET microservices.

Squirrel is a collection of focused NuGet packages that helps .NET teams build microservices faster. Each package handles a common infrastructure concern, such as persistence, messaging, authentication, resilience, health checks, logging, tracing, validation, or caching.

Think of Squirrel as a practical toolkit for microservice-based applications. It is not a single application framework that controls your architecture. Instead, install the packages you need and compose them with standard .NET hosting, dependency injection, configuration, and middleware patterns.

## Why Squirrel?

Most microservices need the same infrastructure setup, but repeating that setup in every service creates inconsistency and maintenance work. Squirrel provides reusable conventions and registration methods so a service can adopt common infrastructure with a small amount of configuration.

- Share contracts and application building blocks across services.
- Add databases, event sourcing, or messaging through standard registrations.
- Apply consistent web API, security, resilience, health-check, and error-handling conventions.
- Configure OpenTelemetry and Serilog without rebuilding observability setup in every service.
- Run local dependencies with .NET Aspire integrations.
- Install only the capabilities required by each service.

## Package overview

Every package is independently installable. The table below explains what each package provides and when to use it.

| Package                                           | What it provides                                                                                                                                                   | When to use it                                                                                                   |
| ------------------------------------------------- | ------------------------------------------------------------------------------------------------------------------------------------------------------------------ | ---------------------------------------------------------------------------------------------------------------- |
| `Squirrel.Abstractions`                     | Shared contracts for commands, queries, events, messages, repositories, paging, persistence, serialization, and web modules.                                       | Use it when application code needs Squirrel contracts without depending on infrastructure implementations. |
| `Squirrel.Core`                             | Common application and domain services, command and query support, options, exceptions, validation helpers, paging, and shared dependency-registration extensions. | Use it as the foundation for services that use Squirrel application building blocks.                       |
| `Squirrel.Web`                              | ASP.NET Core conventions for CORS, compression, API versioning, rate limiting, problem details, minimal APIs, and endpoint mapping.                                | Use it for HTTP APIs that should share consistent web behavior and error responses.                              |
| `Squirrel.Security`                         | JWT bearer authentication and API-key authentication helpers.                                                                                                      | Use it when an API needs standard authentication and authorization registration.                                 |
| `Squirrel.HealthCheck`                      | Default health-check registration and health-check endpoint conventions.                                                                                           | Use it to expose service health for orchestration platforms, load balancers, and monitoring systems.             |
| `Squirrel.Resiliency`                       | Service discovery and HTTP resilience with timeouts, retries, and circuit breakers.                                                                                | Use it when a service calls other services or external HTTP endpoints.                                           |
| `Squirrel.Validation`                       | FluentValidation assembly scanning and dependency-injection registration.                                                                                          | Use it to discover and register request or command validators automatically.                                     |
| `Squirrel.Email`                            | Email delivery abstraction with provider integrations such as SendGrid.                                                                                            | Use it when application code needs to send email without coupling directly to an email provider.                 |
| `Squirrel.Caching`                          | Cache abstractions, hybrid caching support, Redis integration contracts, and cache invalidation patterns.                                                          | Use it for cacheable queries and application-level cache management.                                             |
| `Squirrel.Caching.AzureRedis`               | Azure Redis hosting and client integration.                                                                                                                        | Use it when the service uses Azure Redis or Azure Managed Redis as its cache provider.                           |
| `Squirrel.Serialization`                    | MemoryPack serialization registration for efficient message and data serialization.                                                                                | Use it when shared messages or internal payloads require a fast binary serializer.                               |
| `Squirrel.Integration.Wolverine`            | Wolverine integration with RabbitMQ, handler discovery, message topology, and durable messaging support.                                                           | Use it when a service publishes or consumes commands and events through RabbitMQ.                                |
| `Squirrel.Persistence.EfCore.Postgres`      | PostgreSQL EF Core setup, repositories, unit of work, migrations, naming conventions, retries, and persistence interceptors.                                       | Use it when a service stores relational data in PostgreSQL through EF Core.                                      |
| `Squirrel.Persistence.EfCore.AzurePostgres` | Azure PostgreSQL hosting integration for the EF Core persistence stack.                                                                                            | Use it for services deployed with Azure Database for PostgreSQL.                                                 |
| `Squirrel.Persistence.EfCore.AzureCosmosDB` | Azure Cosmos DB hosting and EF Core Cosmos integration.                                                                                                            | Use it when a service uses Azure Cosmos DB for document-oriented persistence.                                    |
| `Squirrel.Persistence.Mongo`                | MongoDB context, repositories, unit of work, health checks, and tracing integration.                                                                               | Use it when a service stores document data in MongoDB.                                                           |
| `Squirrel.Persistence.Marten`               | Marten document storage and event-sourcing integration on PostgreSQL.                                                                                              | Use it when a service needs Marten documents, event streams, projections, or subscriptions.                      |
| `Squirrel.Persistence.EventStoreDB`         | EventStoreDB client, event-sourcing integration, subscriptions, checkpoints, tracing, and health checks.                                                           | Use it when a service uses EventStoreDB as its event store.                                                      |
| `Squirrel.OpenTelemetry`                    | OpenTelemetry tracing, metrics, logging, exporters, and instrumentation for common dependencies.                                                                   | Use it to establish consistent distributed observability across services.                                        |
| `Squirrel.OpenApi`                          | Swagger, ASP.NET OpenAPI, API versioning documentation, Scalar support, and AsyncAPI support.                                                                      | Use it to document HTTP APIs and message contracts.                                                              |
| `Squirrel.SerilogLogging`                   | Serilog setup, request enrichment, correlation data, baggage enrichment, and common sinks.                                                                         | Use it when services need structured logs with consistent request and trace context.                             |
| `Squirrel.AspireIntegrations`               | .NET Aspire resources for PostgreSQL, MongoDB, Redis, RabbitMQ, EventStoreDB, Elasticsearch, Grafana, Jaeger, Loki, Prometheus, Tempo, Zipkin, and related tools.  | Use it in an Aspire AppHost to run and connect local infrastructure dependencies.                                |

See the [component documentation](docs/components/) and [package reference](docs/reference/packages.md) for detailed setup instructions.

## Typical service setup

Add only the registrations your service needs:

```csharp
var builder = WebApplication.CreateBuilder(args);

builder.AddCoreServices();
builder.AddCustomResiliency();
builder.AddDefaultOpenTelemetry();
builder.AddDefaultHealthChecks();
builder.AddCustomProblemDetails(new[] { typeof(Program).Assembly });
builder.Services.AddCustomValidators(typeof(Program).Assembly);

var app = builder.Build();
app.MapDefaultHealthChecks();
app.Run();
```

Add persistence and messaging separately when required:

```csharp
builder.AddPostgresDbContext<AppDbContext>("catalogdb");
builder.AddWolverineEventBus(
  assemblies: [typeof(OrderPlacedHandler).Assembly]);
```

## Installation

Install the package that owns the capability you need:

```bash
dotnet add package Squirrel.Core
dotnet add package Squirrel.Web
dotnet add package Squirrel.OpenTelemetry
```

For PostgreSQL persistence and RabbitMQ messaging:

```bash
dotnet add package Squirrel.Persistence.EfCore.Postgres
dotnet add package Squirrel.Integration.Wolverine
```

All packages target `net10.0` and use the `Squirrel.*` package and namespace family. Package versions are released together and managed with Nerdbank.GitVersioning.

## Configuration

Squirrel follows normal .NET configuration conventions. Put connection strings and options in `appsettings.json`, environment variables, user secrets, Azure App Configuration, or another configuration provider.

```json
{
  "ConnectionStrings": {
    "catalogdb": "Host=localhost;Database=catalog;Username=postgres;Password=postgres",
    "rabbitmq": "amqp://guest:guest@localhost:5672"
  }
}
```

When using .NET Aspire, connection strings can be injected by the AppHost and consumed by the same registrations.

## Documentation

- [Quickstart](docs/guide/quickstart.md)
- [Architecture](docs/guide/architecture.md)
- [Package reference](docs/reference/packages.md)
- [Detailed component guides](docs/components/)

## License

Squirrel is licensed under the [MIT License](LICENSE).
