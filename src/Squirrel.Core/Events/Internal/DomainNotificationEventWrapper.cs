using Squirrel.Abstractions.Events;

namespace Squirrel.Core.Events.Internal;

public abstract record DomainNotificationEventWrapper<TDomainEventType>(TDomainEventType DomainEvent)
    : DomainNotificationEvent<TDomainEventType>(DomainEvent)
    where TDomainEventType : IDomainEvent;
