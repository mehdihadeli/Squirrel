using Squirrel.Abstractions.Events;

namespace Squirrel.Core.Events;

public class NullDomainEventsAccessor : IDomainEventsAccessor
{
    public IReadOnlyList<IDomainEvent> DequeueUncommittedDomainEvents()
    {
        return new List<IDomainEvent>();
    }
}
