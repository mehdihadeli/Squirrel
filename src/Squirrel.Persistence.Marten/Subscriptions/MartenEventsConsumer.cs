using System.Diagnostics;
using Squirrel.Abstractions.Core.Diagnostics;
using Squirrel.Abstractions.Events;
using Squirrel.Core.Diagnostics;
using Squirrel.Core.Diagnostics.Extensions;
using Squirrel.Persistence.Marten.Extensions;
using Marten;
using Marten.Events;

namespace Squirrel.Persistence.Marten.Subscriptions;

public class MartenEventsConsumer(IInternalEventBus internalEventBus, IActivityRunner activityRunner)
    : IMartenEventsConsumer
{
    public async Task ConsumeAsync(
        IDocumentOperations documentOperations,
        IReadOnlyList<StreamAction> streamActions,
        CancellationToken cancellationToken = default
    )
    {
        foreach (var @event in streamActions.SelectMany(streamAction => streamAction.Events))
        {
            var parentContext = TelemetryPropagator.Extract(@event.Headers, ExtractTraceContextFromEventMetadata);

            var activityName =
                $"{DiagnosticsConstant.Components.Consumer}.{nameof(MartenEventsConsumer)}/{nameof(ConsumeAsync)}";

            await activityRunner
                .ExecuteActivityAsync(
                    new CreateActivityInfo
                    {
                        Name = activityName,
                        ActivityKind = ActivityKind.Consumer,
                        Tags =
                        {
                            { TelemetryTags.Tracing.Event.Type, @event.Data.GetType() },
                            { TelemetryTags.Tracing.Event.Name, @event.Data.GetType().Name },
                        },
                        Parent = parentContext.ActivityContext,
                    },
                    async (_, ct) =>
                    {
                        var streamEvent = @event.ToStreamEvent(parentContext);

                        await internalEventBus.Publish(streamEvent, ct).ConfigureAwait(false);
                    },
                    cancellationToken
                )
                .ConfigureAwait(false);
        }
    }

    private IEnumerable<string> ExtractTraceContextFromEventMetadata(Dictionary<string, object>? headers, string key)
    {
        if (headers!.TryGetValue(key, out var value) != true)
            return [];

        var stringValue = value.ToString();

        return stringValue != null ? [stringValue] : [];
    }
}
