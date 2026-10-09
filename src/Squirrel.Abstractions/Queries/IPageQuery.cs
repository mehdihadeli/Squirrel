using Squirrel.Abstractions.Core.Paging;

namespace Squirrel.Abstractions.Queries;

public interface IPageQuery<out TResponse> : IPageRequest, IQuery<TResponse>
    where TResponse : notnull;
