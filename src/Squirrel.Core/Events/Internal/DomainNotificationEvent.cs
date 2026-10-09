using Squirrel.Abstractions.Events;

namespace Squirrel.Core.Events.Internal;

public abstract record DomainNotificationEvent<TDomainEvent>(TDomainEvent DomainEvent)
    : Event,
        IDomainNotificationEvent<TDomainEvent>
    where TDomainEvent : IDomainEvent;
