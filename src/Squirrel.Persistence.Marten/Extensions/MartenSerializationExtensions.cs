using Squirrel.Abstractions.Messages;
using Squirrel.Abstractions.Persistence.EventStore;
using Squirrel.Core.Messages;
using Squirrel.Core.Persistence.EventStore;
using Marten.Events;
using OpenTelemetry.Context.Propagation;

namespace Squirrel.Persistence.Marten.Extensions;

public static class MartenSerializationExtensions
{
    public static IStreamEventEnvelopeBase ToStreamEvent(
        this IEvent resolvedEvent,
        PropagationContext? propagationContext = null
    )
    {
        var metaData = resolvedEvent.DeserializeMetadata(propagationContext);

        return StreamEventEnvelopeFactory.From(resolvedEvent.Data, metaData);
    }

    public static StreamEventMetadata DeserializeMetadata(
        this IEvent resolvedEvent,
        PropagationContext? propagationContext = null
    )
    {
        var eventMetadata = new StreamEventMetadata(
            resolvedEvent.Id.ToString(),
            (ulong)resolvedEvent.Version,
            (ulong)resolvedEvent.Sequence,
            propagationContext
        );

        return eventMetadata;
    }
}
