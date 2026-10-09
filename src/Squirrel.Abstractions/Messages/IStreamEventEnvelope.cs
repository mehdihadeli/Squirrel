using Squirrel.Abstractions.Events;

namespace Squirrel.Abstractions.Messages;

public interface IStreamEventEnvelope<out T> : IStreamEventEnvelopeBase
    where T : class, IDomainEvent
{
    new T Data { get; }
}
