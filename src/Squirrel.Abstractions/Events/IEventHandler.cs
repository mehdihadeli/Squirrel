using Mediator;

namespace Squirrel.Abstractions.Events;

public interface IEventHandler<in TEvent> : INotificationHandler<TEvent>
    where TEvent : INotification;
