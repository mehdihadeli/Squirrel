# Squirrel.Persistence.EfCore.AzurePostgres

Azure PostgreSQL package area for EF Core persistence.

## Install

```bash
dotnet add package Squirrel.Persistence.EfCore.AzurePostgres
```

Use this package when the deployment target is Azure Database for PostgreSQL and the service needs Azure-specific persistence configuration. Pair it with `Squirrel.Persistence.EfCore.Postgres` for the shared DbContext, repository, unit-of-work, and migration conventions.

> This package is currently a placeholder in the repository. The PostgreSQL implementation is provided by `Squirrel.Persistence.EfCore.Postgres`.
