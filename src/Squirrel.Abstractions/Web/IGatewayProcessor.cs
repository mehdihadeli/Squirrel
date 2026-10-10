using Squirrel.Abstractions.Commands;
using Squirrel.Abstractions.Messages;
using Squirrel.Abstractions.Queries;
using Squirrel.Abstractions.Web.Module;

namespace Squirrel.Abstractions.Web;

public interface IGatewayProcessor<TModule>
    where TModule : class, IModuleConfiguration
{
    ValueTask ExecuteScopeAsync(Func<IServiceProvider, ValueTask> action);
    ValueTask<TResult> ExecuteScopeAsync<TResult>(Func<IServiceProvider, ValueTask<TResult>> action);
    Task<TResponse> SendCommandAsync<TResponse>(ICommand<TResponse> request, CancellationToken cancellationToken = default)
        where TResponse : notnull;
    Task SendCommandAsync<TCommand>(TCommand request, CancellationToken cancellationToken = default)
        where TCommand : class, ICommand;
    Task ExecuteCommand(Func<ICommandBus, Task> action);
    Task<TResult> ExecuteCommand<TResult>(Func<ICommandBus, Task<TResult>> action);
    Task<TResponse> SendQueryAsync<TResponse>(IQuery<TResponse> query, CancellationToken cancellationToken = default)
        where TResponse : notnull;
    Task<TResult> ExecuteQuery<TResult>(Func<IQueryBus, Task<TResult>> action);
    Task Publish(Func<IExternalEventBus, Task> action);
}