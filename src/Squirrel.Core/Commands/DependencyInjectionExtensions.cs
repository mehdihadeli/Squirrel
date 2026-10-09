using Squirrel.Abstractions.Commands;
using Squirrel.Abstractions.Scheduler;
using Squirrel.Core.Commands.Diagnostics;
using Squirrel.Core.Scheduler;

namespace Squirrel.Core.Commands;

internal static class DependencyInjectionExtensions
{
    public static IServiceCollection AddCommandBus(this IServiceCollection services)
    {
        services.AddTransient<ICommandBus, CommandBus>();
        services.AddTransient<IAsyncCommandBus, AsyncCommandBus>();
        services.AddTransient<ICommandScheduler, NullCommandScheduler>();

        services.AddTransient<CommandHandlerActivity>();
        services.AddTransient<CommandHandlerMetrics>();

        return services;
    }
}
