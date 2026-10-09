# Development

## Repository layout

```text
Squirrel/
├── src/
│   └── Squirrel.*
├── tests/
│   ├── Squirrel.Component/
│   │   ├── Squirrel.Component.UnitTests/
│   │   └── Squirrel.Component.IntegrationTests/
│   └── Shared/
└── docs/
```

Every source component has matching unit and integration test projects. Empty projects are intentional and provide a stable place for future tests.

## Versioning

Versions come from `version.json` through Nerdbank.GitVersioning. Release builds should use full Git history and set `PublicRelease=true` when producing stable packages.

```bash
dotnet pack src/Squirrel.Core/Squirrel.Core.csproj -c Release -p:PublicRelease=true
```

See [Versioning and releases](./versioning) for the preview, RC, stable, and
GitHub Actions publication flow.

## Pull requests

Keep changes focused on one component, add tests in its matching test project, and update documentation when public package behavior changes. Do not introduce a second versioning system or hard-code package versions.
