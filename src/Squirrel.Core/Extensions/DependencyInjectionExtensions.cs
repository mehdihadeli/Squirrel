using System.Reflection;
using Squirrel.Core.Commands;
using Squirrel.Core.Events.Extensions;
using Squirrel.Core.Messages;
using Squirrel.Core.Messages.Extensions;
using Squirrel.Core.Paging;
using Squirrel.Core.Persistence;
using Squirrel.Core.Queries;
using Squirrel.Core.Serialization;
using Squirrel.Core.Serialization.NewtonsoftSerializer;
using Mediator;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.Extensions.Hosting;
using Sieve.Services;

namespace Squirrel.Core.Extensions;

public static class DependencyInjectionExtensions
{
    public static IHostApplicationBuilder AddCoreServices(this IHostApplicationBuilder builder)
    {
        // Find assemblies that reference the current assembly
        var referencingAssemblies = Assembly.GetCallingAssembly().GetReferencingAssemblies();

        builder.Services.TryAddScoped<ISieveProcessor, ApplicationSieveProcessor>();

        builder.Services.AddDefaultSerializer();

        builder.Services.AddCommandBus();

        builder.Services.AddQueryBus();

        builder.Services.AddEvents(referencingAssemblies);

        builder.Services.AddMessages(referencingAssemblies);

        builder.Services.ScanAndRegisterDbExecutors(referencingAssemblies);

        builder.Services.TryAddScoped<IMediator, NullMediator>();

        return builder;
    }
}
