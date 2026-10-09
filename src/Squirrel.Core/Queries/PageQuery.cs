namespace Squirrel.Core.Queries;

using Squirrel.Abstractions.Queries;
using Squirrel.Core.Paging;

// https://learn.microsoft.com/en-us/dotnet/csharp/whats-new/tutorials/records#characteristics-of-records
public record PageQuery<TResponse> : PageRequest, IPageQuery<TResponse>
    where TResponse : notnull;
