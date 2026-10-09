namespace Squirrel.Abstractions.Events;

public interface IDomainEventContext
{
    IReadOnlyList<IDomainEvent> DequeueUncommittedDomainEvents();
}
