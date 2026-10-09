using Squirrel.Abstractions.Persistence;
using Mediator;

namespace Squirrel.Abstractions.Commands;

public interface ITxCommand : ITxCommand<Unit>;

public interface ITxCommand<out T> : ICommand<T>, ITxRequest
    where T : notnull;
