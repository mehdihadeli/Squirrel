using Squirrel.Abstractions.Commands;
using Squirrel.Abstractions.Events;
using Squirrel.Abstractions.Messages;
using Squirrel.Abstractions.Messages.MessagePersistence;
using Microsoft.Extensions.Logging;
using Wolverine;
using IMessage = Squirrel.Abstractions.Messages.IMessage;

namespace Squirrel.Integration.Wolverine.InMemory;

public sealed class WolverineInMemoryMessagePersistenceService(
    IMessageBus bus,
    ILogger<WolverineInMemoryMessagePersistenceService> logger
) : IMessagePersistenceService
{
    public async Task AddPublishMessageAsync(IMessageEnvelopeBase messageEnvelope, CancellationToken cancellationToken = default)
    {
        var method = typeof(WolverineInMemoryMessagePersistenceService)
            .GetMethod(nameof(PublishCoreAsync), System.Reflection.BindingFlags.Instance | System.Reflection.BindingFlags.NonPublic)!;
        var genericMethod = method.MakeGenericMethod(messageEnvelope.Message.GetType());
        await (Task)genericMethod.Invoke(this, [messageEnvelope, cancellationToken])!;
        logger.LogDebug("Published {MessageType} through Wolverine in-memory messaging.", messageEnvelope.Message.GetType().Name);
    }

    public Task AddReceivedMessageAsync<TMessage>(IMessageEnvelopeBase messageEnvelope, Func<IMessageEnvelopeBase, Task> dispatchAction, CancellationToken cancellationToken = default)
        => dispatchAction(messageEnvelope);

    public Task AddInternalMessageAsync<TInternalCommand>(TInternalCommand internalCommand, CancellationToken cancellationToken = default)
        where TInternalCommand : IInternalCommand => bus.SendAsync(internalCommand).AsTask();

    public Task AddNotificationAsync<TDomainNotification>(TDomainNotification notification, CancellationToken cancellationToken = default)
        where TDomainNotification : IDomainNotificationEvent<IDomainEvent> => bus.SendAsync(notification).AsTask();

    private Task PublishCoreAsync<TMessage>(IMessageEnvelopeBase envelope, CancellationToken cancellationToken)
        where TMessage : class, IMessage =>
        bus.PublishAsync((TMessage)envelope.Message, WolverineInMemoryDeliveryOptionsFactory.Build(envelope)).AsTask();
}