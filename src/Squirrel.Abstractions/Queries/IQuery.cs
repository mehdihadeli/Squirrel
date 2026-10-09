using Mediator;

namespace Squirrel.Abstractions.Queries;

public interface IQueryBase;

public interface IStreamQueryBase;

public interface IQuery<out TResponse> : IQueryBase, IRequest<TResponse>
    where TResponse : notnull;

public interface IStreamQuery<out T> : IStreamQueryBase, IStreamRequest<T>
    where T : notnull;
