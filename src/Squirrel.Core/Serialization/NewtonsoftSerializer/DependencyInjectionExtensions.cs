using Squirrel.Abstractions.Serialization;
using Newtonsoft.Json;

namespace Squirrel.Core.Serialization.NewtonsoftSerializer;

internal static class DependencyInjectionExtensions
{
    internal static void AddDefaultSerializer(
        this IServiceCollection services,
        Action<JsonSerializerSettings>? configuration = null
    )
    {
        var defaultSettings = NewtonsoftSerializerSerializerOptions.DefaultSerializerSettings;
        configuration?.Invoke(defaultSettings);

        services.AddTransient<ISerializer>(_ => new NewtonsoftObjectSerializer(defaultSettings));
        services.AddTransient<IMessageSerializer>(_ => new NewtonsoftMessageSerializer(defaultSettings));
    }
}
