using Squirrel.Abstractions.Queries;
using Squirrel.Abstractions.Web.MinimalApi;
using Microsoft.AspNetCore.Http;

namespace Squirrel.Web.Minimal;

public record HttpQuery(HttpContext HttpContext, IQueryBus QueryBus, CancellationToken CancellationToken) : IHttpQuery;
