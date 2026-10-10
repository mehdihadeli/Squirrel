using Squirrel.Abstractions.Web.Module;
using Microsoft.Extensions.DependencyInjection;

namespace Squirrel.Web.Module;

public sealed class CompositionRoot(
    IServiceProvider serviceProvider,
    IModuleConfiguration module
) : ICompositionRoot
{
    public IServiceProvider ServiceProvider { get; } = serviceProvider;
    public IModuleConfiguration ModuleDefinition { get; } = module;

    public IServiceScope CreateScope() => ServiceProvider.CreateScope();
}