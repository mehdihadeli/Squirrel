using Squirrel.Abstractions.Commands;
using Squirrel.Abstractions.Messages;
using Squirrel.Abstractions.Queries;
using Squirrel.Abstractions.Web;
using Squirrel.Abstractions.Web.Module;
using Squirrel.Web.Module;
using Microsoft.Extensions.DependencyInjection;

namespace Squirrel.Web;

public sealed class GatewayProcessor<TModule>(IServiceProvider serviceProvider) : IGatewayProcessor<TModule>
    where TModule : class, IModuleConfiguration
{
    private IServiceProvider ModuleServiceProvider =>
        CompositionRootRegistry.GetByModule<TModule>()?.ServiceProvider ?? serviceProvider;

    public async ValueTask ExecuteScopeAsync(Func<IServiceProvider, ValueTask> action)
    {
        await using var scope = ModuleServiceProvider.CreateAsyncScope();
        await action(scope.ServiceProvider);
    }

    public async ValueTask<TResult> ExecuteScopeAsync<TResult>(Func<IServiceProvider, ValueTask<TResult>> action)
    {
        await using var scope = ModuleServiceProvider.CreateAsyncScope();
        return await action(scope.ServiceProvider);
    }

    public async Task<TResponse> SendCommandAsync<TResponse>(
        ICommand<TResponse> request,
        CancellationToken cancellationToken = default
    ) where TResponse : notnull =>
        await ExecuteScopeAsync(async provider =>
            await provider.GetRequiredService<ICommandBus>().SendAsync(request, cancellationToken));

    public async Task SendCommandAsync<TCommand>(
        TCommand request,
        CancellationToken cancellationToken = default
    ) where TCommand : class, ICommand =>
        await ExecuteScopeAsync(async provider =>
            await provider.GetRequiredService<ICommandBus>().SendAsync(request, cancellationToken));

    public async Task ExecuteCommand(Func<ICommandBus, Task> action) =>
        await ExecuteScopeAsync(async provider => await action(provider.GetRequiredService<ICommandBus>()));

    public async Task<TResult> ExecuteCommand<TResult>(Func<ICommandBus, Task<TResult>> action) =>
        await ExecuteScopeAsync(async provider => await action(provider.GetRequiredService<ICommandBus>()));

    public async Task<TResponse> SendQueryAsync<TResponse>(
        IQuery<TResponse> query,
        CancellationToken cancellationToken = default
    ) where TResponse : notnull =>
        await ExecuteScopeAsync(async provider =>
            await provider.GetRequiredService<IQueryBus>().SendAsync(query, cancellationToken));

    public async Task<TResult> ExecuteQuery<TResult>(Func<IQueryBus, Task<TResult>> action) =>
        await ExecuteScopeAsync(async provider => await action(provider.GetRequiredService<IQueryBus>()));

    public async Task Publish(Func<IExternalEventBus, Task> action) =>
        await ExecuteScopeAsync(async provider => await action(provider.GetRequiredService<IExternalEventBus>()));
}