using System.Diagnostics;
using System.Diagnostics.Metrics;
using Squirrel.Abstractions.Core.Diagnostics;
using Microsoft.Extensions.Hosting;

namespace Squirrel.Core.Diagnostics.Extensions;

public static class DependencyInjectionExtensions
{
    public static IHostApplicationBuilder AddDiagnostics(
        this IHostApplicationBuilder builder,
        string instrumentationName
    )
    {
        Activity.DefaultIdFormat = ActivityIdFormat.W3C;

        TelemetryTags.Configure(instrumentationName);

        builder.Services.AddSingleton<IDiagnosticsProvider>(sp =>
        {
            var meterFactory = sp.GetRequiredService<IMeterFactory>();

            return new DiagnosticsProvider(meterFactory, instrumentationName);
        });

        builder.Services.AddSingleton<IActivityRunner, ActivityRunner>();

        return builder;
    }
}
