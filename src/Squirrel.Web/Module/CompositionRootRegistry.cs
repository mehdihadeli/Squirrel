using System.Collections.Concurrent;
using Squirrel.Abstractions.Web.Module;

namespace Squirrel.Web.Module;

public static class CompositionRootRegistry
{
    private static readonly ConcurrentDictionary<Type, ICompositionRoot> Roots = new();

    public static IReadOnlyList<ICompositionRoot> CompositionRoots => Roots.Values.ToArray();

    public static IServiceProvider RootServiceProvider { get; private set; } = null!;

    public static void SetRootServiceProvider(IServiceProvider serviceProvider) =>
        RootServiceProvider = serviceProvider;

    public static void Add(ICompositionRoot compositionRoot) =>
        Roots[compositionRoot.ModuleDefinition.GetType()] = compositionRoot;

    public static void Remove(ICompositionRoot compositionRoot) =>
        ((ICollection<KeyValuePair<Type, ICompositionRoot>>)Roots).Remove(
            new KeyValuePair<Type, ICompositionRoot>(compositionRoot.ModuleDefinition.GetType(), compositionRoot)
        );

    public static ICompositionRoot? GetByModuleByAssemblyName(string assemblyName) =>
        Roots.Values.FirstOrDefault(root => root.ModuleDefinition.GetType().Assembly.GetName().Name == assemblyName);

    public static ICompositionRoot? GetByModule(IModuleConfiguration moduleDefinition) =>
        Roots.TryGetValue(moduleDefinition.GetType(), out var root) && ReferenceEquals(root.ModuleDefinition, moduleDefinition)
            ? root
            : null;

    public static ICompositionRoot? GetByModule<TModule>()
        where TModule : class, IModuleConfiguration =>
        Roots.TryGetValue(typeof(TModule), out var root) ? root : null;
}