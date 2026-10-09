using System.Reflection;
using Squirrel.Abstractions.Persistence;
using Squirrel.Core.Extensions;

namespace Squirrel.Core.Persistence;

public static class DbExecutersExtensions
{
    public static void ScanAndRegisterDbExecutors(this IServiceCollection services, params Assembly[] assembliesToScan)
    {
        var dbExecutors = assembliesToScan
            .SelectMany(x => x.GetLoadableTypes())
            .Where(t =>
                t!.IsClass
                && !t.IsAbstract
                && !t.IsGenericType
                && !t.IsInterface
                && t.GetConstructor(Type.EmptyTypes) != null
                && typeof(IDbExecutors).IsAssignableFrom(t)
            )
            .ToList();

        foreach (var dbExecutor in dbExecutors)
        {
            var instantiatedType = (IDbExecutors)Activator.CreateInstance(dbExecutor)!;
            instantiatedType.Register(services);
        }
    }
}
