using Squirrel.Abstractions.Events;
using Squirrel.Abstractions.Messages;

namespace Squirrel.Core.Messages;

public record StreamEventEnvelope<T>(T Data, StreamEventMetadata? Metadata) : IStreamEventEnvelope<T>
    where T : class, IDomainEvent
{
    object IStreamEventEnvelopeBase.Data => Data;
}
