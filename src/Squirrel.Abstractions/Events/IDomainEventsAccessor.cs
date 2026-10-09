namespace Squirrel.Abstractions.Events;

public interface IDomainEventsAccessor
{
    IReadOnlyList<IDomainEvent> DequeueUncommittedDomainEvents();
}
