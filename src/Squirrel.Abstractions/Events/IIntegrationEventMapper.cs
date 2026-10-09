using Squirrel.Abstractions.Messages;

namespace Squirrel.Abstractions.Events;

public interface IIntegrationEventMapper
{
    IIntegrationEvent? MapToIntegrationEvent(IDomainEvent domainEvent);
}
