using Microsoft.Extensions.DependencyInjection;

namespace Squirrel.Abstractions.Web.Module;

public interface ICompositionRoot
{
    IServiceProvider ServiceProvider { get; }
    IModuleConfiguration ModuleDefinition { get; }
    IServiceScope CreateScope();
}