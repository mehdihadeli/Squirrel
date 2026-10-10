using System.Reflection;
using Squirrel.Abstractions.Messages;
using Squirrel.Abstractions.Messages.MessagePersistence;
using Squirrel.Core.Extensions;
using Squirrel.Core.Extensions.HostApplicationBuilderExtensions;
using Squirrel.Core.Extensions.ServiceCollectionExtensions;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.Extensions.Hosting;
using Wolverine;
using Wolverine.EntityFrameworkCore;
using Wolverine.Postgresql;

namespace Squirrel.Integration.Wolverine.InMemory.Extensions;

public static class DependencyInjectionExtensions
{
    public static IHostApplicationBuilder AddWolverineInMemoryEventBus(
        this IHostApplicationBuilder builder,
        Action<WolverineOptions>? configureBus = null,
        Action<WolverineInMemoryBusOptions>? configureOptions = null,
        string? durabilityConnectionStringName = null,
        Assembly[]? assemblies = null
    )
    {
        assemblies ??= [Assembly.GetCallingAssembly()];
        builder.Services.AddValidationOptions(configurator: configureOptions);
        var options = builder.Configuration.BindOptions<WolverineInMemoryBusOptions>();

        builder.Services.Replace(ServiceDescriptor.Transient<IExternalEventBus, WolverineInMemoryEventBus>());
        builder.Services.Replace(ServiceDescriptor.Scoped<IMessagePersistenceService, WolverineInMemoryMessagePersistenceService>());

        builder.Services.AddWolverine(wolverine =>
        {
            foreach (var assembly in assemblies)
            {
                wolverine.Discovery.IncludeAssembly(assembly);
            }

            wolverine.UseRuntimeCompilation();
            configureBus?.Invoke(wolverine);

            if (options.EnableDurability)
            {
                var connectionString = options.DurabilityConnectionString
                    ?? builder.Configuration.GetConnectionString(durabilityConnectionStringName ?? "wolverine");

                if (string.IsNullOrWhiteSpace(connectionString))
                {
                    throw new InvalidOperationException("A PostgreSQL connection string is required when Wolverine in-memory durability is enabled.");
                }

                wolverine.PersistMessagesWithPostgresql(connectionString);
                if (options.UseEntityFrameworkCoreTransactions)
                {
                    wolverine.UseEntityFrameworkCoreTransactions();
                }
            }

            if (options.UseDurableLocalQueues && options.EnableDurability)
            {
                wolverine.Policies.UseDurableLocalQueues();
            }
        });

        return builder;
    }
}