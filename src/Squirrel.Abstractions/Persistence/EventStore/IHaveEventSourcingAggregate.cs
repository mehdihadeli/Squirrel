using Squirrel.Abstractions.Domain;
using Squirrel.Abstractions.Domain.EventSourcing;
using Squirrel.Abstractions.Events;
using Squirrel.Abstractions.Persistence.EventStore.Projections;

namespace Squirrel.Abstractions.Persistence.EventStore;

public interface IHaveEventSourcingAggregate
    : IHaveAggregateStateProjection,
        IAggregateBase,
        IHaveEventSourcedAggregateVersion
{
    /// <summary>
    ///     Loads the current state of the aggregate from a list of events.
    /// </summary>
    /// <param name="history">Domain events from the aggregate stream.</param>
    void LoadFromHistory(IEnumerable<IDomainEvent> history);
}
