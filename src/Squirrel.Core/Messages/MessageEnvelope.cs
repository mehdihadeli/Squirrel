namespace Squirrel.Core.Messages;

using Squirrel.Abstractions.Messages;

// using `IEventEnvelopeMetadata` has problem in deserialize construction so we have to use `MessageEnvelopeMetadata`
public record MessageEnvelope<T>(T Message, MessageEnvelopeMetadata Metadata) : IMessageEnvelope<T>
    where T : class, IMessage
{
    object IMessageEnvelopeBase.Message => Message;
}
