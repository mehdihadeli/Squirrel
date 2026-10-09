namespace Squirrel.Core.Messages;

using Squirrel.Abstractions.Messages;

public abstract record Message : IMessage
{
    public Guid MessageId => Guid.CreateVersion7();
    public DateTime Created { get; } = DateTime.Now;
}
