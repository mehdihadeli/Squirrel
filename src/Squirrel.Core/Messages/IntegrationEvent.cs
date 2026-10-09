namespace Squirrel.Core.Messages;

using Squirrel.Abstractions.Messages;

public abstract record IntegrationEvent : Message, IIntegrationEvent;
