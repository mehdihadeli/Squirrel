namespace Squirrel.Core.Queries;

using Squirrel.Abstractions.Queries;

public abstract class QueryHandler<TQuery, TResponse> : IQueryHandler<TQuery, TResponse>
    where TQuery : IQuery<TResponse>
    where TResponse : notnull
{
    protected abstract ValueTask<TResponse> HandleQueryAsync(
        TQuery query,
        CancellationToken cancellationToken = default
    );

    public ValueTask<TResponse> Handle(TQuery request, CancellationToken cancellationToken)
    {
        return HandleQueryAsync(request, cancellationToken);
    }
}
