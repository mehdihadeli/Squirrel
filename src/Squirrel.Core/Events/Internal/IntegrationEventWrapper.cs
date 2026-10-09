using Squirrel.Abstractions.Events;
using Squirrel.Core.Messages;

namespace Squirrel.Core.Events.Internal;

public record IntegrationEventWrapper<TDomainEventType>(TDomainEventType DomainEvent) : IntegrationEvent
    where TDomainEventType : IDomainEvent;
