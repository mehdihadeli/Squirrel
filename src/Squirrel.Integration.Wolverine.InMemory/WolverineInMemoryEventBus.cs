using Squirrel.Abstractions.Messages;
using Squirrel.Abstractions.Messages.MessagePersistence;
using Squirrel.Core;
using Squirrel.Core.Messages;
using Humanizer;
using Microsoft.Extensions.Options;
using Wolverine;
using IMessage = Squirrel.Abstractions.Messages.IMessage;

namespace Squirrel.Integration.Wolverine.InMemory;

public sealed class WolverineInMemoryEventBus(
    IMessageBus bus,
    IMessageMetadataAccessor metadataAccessor,
    IMessagePersistenceService persistence,
    IOptions<MessagingOptions> messagingOptions
) : IExternalEventBus
{
    public Task PublishAsync<TMessage>(TMessage message, CancellationToken cancellationToken = default)
        where TMessage : class, IMessage => PublishAsync(CreateEnvelope(message), cancellationToken);

    public Task PublishAsync<TMessage>(
        IMessageEnvelope<TMessage> messageEnvelope,
        CancellationToken cancellationToken = default
    ) where TMessage : class, IMessage => PublishEnvelopeAsync(messageEnvelope, cancellationToken);

    public Task PublishAsync(IMessageEnvelopeBase messageEnvelope, CancellationToken cancellationToken = default) =>
        PublishEnvelopeAsync(messageEnvelope, cancellationToken);

    public Task PublishAsync<TMessage>(
        TMessage message,
        string? exchangeOrTopic = null,
        string? queue = null,
        CancellationToken cancellationToken = default
    ) where TMessage : class, IMessage => PublishAsync(message, cancellationToken);

    public Task PublishAsync<TMessage>(
        IMessageEnvelope<TMessage> messageEnvelope,
        string? exchangeOrTopic = null,
        string? queue = null,
        CancellationToken cancellationToken = default
    ) where TMessage : class, IMessage => PublishEnvelopeAsync(messageEnvelope, cancellationToken);

    private Task PublishEnvelopeAsync(IMessageEnvelopeBase envelope, CancellationToken cancellationToken)
    {
        if (messagingOptions.Value.OutboxEnabled)
        {
            return persistence.AddPublishMessageAsync(envelope, cancellationToken);
        }

        return PublishNowAsync(envelope, cancellationToken);
    }

    internal async Task PublishNowAsync(IMessageEnvelopeBase envelope, CancellationToken cancellationToken)
    {
        var method = typeof(WolverineInMemoryEventBus)
            .GetMethod(nameof(PublishNowCoreAsync), System.Reflection.BindingFlags.Instance | System.Reflection.BindingFlags.NonPublic)!;
        var genericMethod = method.MakeGenericMethod(envelope.Message.GetType());
        await (Task)genericMethod.Invoke(this, [envelope, cancellationToken])!;
    }

    private Task PublishNowCoreAsync<TMessage>(IMessageEnvelopeBase envelope, CancellationToken cancellationToken)
        where TMessage : class, IMessage =>
        bus.PublishAsync((TMessage)envelope.Message, WolverineInMemoryDeliveryOptionsFactory.Build(envelope)).AsTask();

    private IMessageEnvelope<TMessage> CreateEnvelope<TMessage>(TMessage message)
        where TMessage : class, IMessage
    {
        var messageTypeName = message.GetType().Name.Underscore();
        return MessageEnvelopeFactory.From(
            message,
            metadataAccessor.GetCorrelationId(),
            message.MessageId,
            new Dictionary<string, object?>
            {
                [MessageHeaders.Name] = messageTypeName,
                [MessageHeaders.Queue] = messageTypeName,
            }
        );
    }
}