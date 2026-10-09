# Squirrel.AspireIntegrations

.NET Aspire AppHost resources for local and containerized development infrastructure.

## Install

```bash
dotnet add package Squirrel.AspireIntegrations
```

## Example

```csharp
var builder = DistributedApplication.CreateBuilder(args);

var postgres = builder.AddAspirePostgresDatabase("catalogdb");
var redis = builder.AddAspireRedis("cache");
var rabbitmq = builder.AddAspireRabbitmq("messaging");
var jaeger = builder.AddAspireJaeger("traces");

builder.Build().Run();
```

The package includes resources for PostgreSQL, MongoDB, Redis, RabbitMQ, EventStoreDB, Elasticsearch, Grafana, Jaeger, Loki, Prometheus, Tempo, Zipkin, Kibana, OpenTelemetry Collector, and HealthChecks UI. Resource extensions also support data volumes, bind mounts, provisioning files, and project references.

Use it from an Aspire AppHost and keep production infrastructure configuration separate from local orchestration.
