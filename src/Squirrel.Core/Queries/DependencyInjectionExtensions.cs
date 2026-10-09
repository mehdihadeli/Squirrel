using Squirrel.Abstractions.Queries;
using Squirrel.Core.Queries.Diagnostics;

namespace Squirrel.Core.Queries;

internal static class DependencyInjectionExtensions
{
    internal static IServiceCollection AddQueryBus(this IServiceCollection services)
    {
        services.AddTransient<IQueryBus, QueryBus>();

        services.AddTransient<QueryHandlerActivity>();
        services.AddTransient<QueryHandlerMetrics>();

        return services;
    }
}
