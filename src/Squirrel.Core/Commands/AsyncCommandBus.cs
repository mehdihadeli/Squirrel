using Squirrel.Abstractions.Commands;
using Squirrel.Abstractions.Messages;
using Squirrel.Abstractions.Messages.MessagePersistence;
using Squirrel.Core.Messages;

namespace Squirrel.Core.Commands;

public class AsyncCommandBus(IServiceProvider serviceProvider, IMessageMetadataAccessor messageMetadataAccessor)
    : IAsyncCommandBus
{
    public Task SendExternalAsync<TCommand>(TCommand command, CancellationToken cancellationToken = default)
        where TCommand : class, IAsyncCommand
    {
        var correlationId = messageMetadataAccessor.GetCorrelationId();
        var cautionId = command.MessageId;
        var eventEnvelope = MessageEnvelopeFactory.From(command, correlationId, cautionId);

        var messagePersistenceService = serviceProvider.GetRequiredService<IMessagePersistenceService>();
        return messagePersistenceService.AddPublishMessageAsync(eventEnvelope, cancellationToken);
    }
}
