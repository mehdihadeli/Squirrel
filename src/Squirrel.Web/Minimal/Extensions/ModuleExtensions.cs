using System.Reflection;
using Squirrel.Abstractions.Web.Module;
using Squirrel.Core.Extensions;
using Squirrel.Web.Module;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Routing;
using Microsoft.Extensions.Configuration;

namespace Squirrel.Web.Minimal.Extensions;

public static class ModuleExtensions
{
    public static WebApplicationBuilder AddModulesServices(
        this WebApplicationBuilder builder,
        bool useCompositionRootForModules,
        params Assembly[] scanAssemblies
    )
    {
        if (!useCompositionRootForModules)
            return builder.AddModulesServices(scanAssemblies);

        var moduleTypes = scanAssemblies.SelectMany(assembly => assembly.GetLoadableTypes())
            .Where(type => type is { IsClass: true, IsAbstract: false }
                && typeof(IModuleConfiguration).IsAssignableFrom(type)
                && type.GetConstructor(Type.EmptyTypes) != null);

        foreach (var moduleType in moduleTypes)
        {
            var module = (IModuleConfiguration)Activator.CreateInstance(moduleType!)!;
            var moduleBuilder = WebApplication.CreateBuilder(new WebApplicationOptions
            {
                EnvironmentName = builder.Environment.EnvironmentName,
                ContentRootPath = builder.Environment.ContentRootPath,
                ApplicationName = builder.Environment.ApplicationName,
            });
            moduleBuilder.Configuration.AddConfiguration(builder.Configuration);
            foreach (var descriptor in builder.Services)
                moduleBuilder.Services.Add(descriptor);

            module.AddModuleServices(moduleBuilder);
            var provider = moduleBuilder.Services.BuildServiceProvider(new ServiceProviderOptions
            {
                ValidateScopes = true,
            });
            var root = new CompositionRoot(provider, module);
            CompositionRootRegistry.Add(root);
            builder.Services.AddSingleton<IModuleConfiguration>(module);
            builder.Services.AddSingleton<ICompositionRoot>(root);
        }

        return builder;
    }

    public static WebApplicationBuilder AddModulesServices(
        this WebApplicationBuilder webApplicationBuilder,
        params Assembly[] scanAssemblies
    )
    {
        if (scanAssemblies.Length == 0)
        {
            // Find assemblies that reference the current assembly
            var referencingAssemblies = Assembly.GetExecutingAssembly().GetReferencingAssemblies();
            scanAssemblies = referencingAssemblies.ToArray();
        }

        var modulesConfiguration = scanAssemblies
            .SelectMany(x => x.GetLoadableTypes())
            .Where(t =>
                t!.IsClass
                && !t.IsAbstract
                && !t.IsGenericType
                && !t.IsInterface
                && t.GetConstructor(Type.EmptyTypes) != null
                && typeof(IModuleConfiguration).IsAssignableFrom(t)
            )
            .ToList();

        var sharedModulesConfiguration = scanAssemblies
            .SelectMany(x => x.GetLoadableTypes())
            .Where(t =>
                t!.IsClass
                && !t.IsAbstract
                && !t.IsGenericType
                && !t.IsInterface
                && t.GetConstructor(Type.EmptyTypes) != null
                && typeof(ISharedModulesConfiguration).IsAssignableFrom(t)
            )
            .ToList();

        foreach (var sharedModule in sharedModulesConfiguration)
        {
            AddModulesDependencyInjection(webApplicationBuilder, sharedModule);
        }

        foreach (var module in modulesConfiguration)
        {
            AddModulesDependencyInjection(webApplicationBuilder, module);
        }

        return webApplicationBuilder;
    }

    private static void AddModulesDependencyInjection(WebApplicationBuilder webApplicationBuilder, Type module)
    {
        if (module.IsAssignableTo(typeof(IModuleConfiguration)))
        {
            var instantiatedType = (IModuleConfiguration)Activator.CreateInstance(module)!;
            instantiatedType.AddModuleServices(webApplicationBuilder);
            webApplicationBuilder.Services.AddSingleton(instantiatedType);
        }

        if (module.IsAssignableTo(typeof(ISharedModulesConfiguration)))
        {
            var instantiatedType = (ISharedModulesConfiguration)Activator.CreateInstance(module)!;
            instantiatedType.AddSharedModuleServices(webApplicationBuilder);
            webApplicationBuilder.Services.AddSingleton(instantiatedType);
        }
    }

    public static async Task<WebApplication> ConfigureModules(this WebApplication app)
    {
        CompositionRootRegistry.SetRootServiceProvider(app.Services);
        var moduleConfigurations = app.Services.GetServices<IModuleConfiguration>();
        var sharedModulesConfigurations = app.Services.GetServices<ISharedModulesConfiguration>();

        foreach (var sharedModule in sharedModulesConfigurations)
        {
            await sharedModule.ConfigureSharedModule(app);
        }

        foreach (var module in moduleConfigurations)
        {
            await module.ConfigureModule(app);
        }

        return app;
    }

    public static IEndpointRouteBuilder MapModulesEndpoints(this IEndpointRouteBuilder builder)
    {
        var modules = builder.ServiceProvider.GetServices<IModuleConfiguration>();
        var sharedModules = builder.ServiceProvider.GetServices<ISharedModulesConfiguration>();

        foreach (var module in sharedModules)
        {
            module.MapSharedModuleEndpoints(builder);
        }

        foreach (var module in modules)
        {
            module.MapEndpoints(builder);
        }

        return builder;
    }
}
