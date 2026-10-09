using Squirrel.Abstractions.Events;
using Squirrel.Abstractions.Messages;

namespace Squirrel.Abstractions.Persistence.EventStore.Projections;

public interface IHaveReadProjection
{
    Task ProjectAsync<T>(IStreamEventEnvelope<T> iStreamEvent, CancellationToken cancellationToken = default)
        where T : class, IDomainEvent;
}
