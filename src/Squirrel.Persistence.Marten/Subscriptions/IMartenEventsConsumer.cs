using Marten;
using Marten.Events;

namespace Squirrel.Persistence.Marten.Subscriptions;

public interface IMartenEventsConsumer
{
    Task ConsumeAsync(
        IDocumentOperations documentOperations,
        IReadOnlyList<StreamAction> streamActions,
        CancellationToken cancellationToken = default
    );
}
