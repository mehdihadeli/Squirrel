using Squirrel.Abstractions.Events;

namespace Squirrel.Core.Events;

public class NullIDomainEventContext : IDomainEventContext
{
    public IReadOnlyList<IDomainEvent> DequeueUncommittedDomainEvents()
    {
        return new List<IDomainEvent>();
    }
}
