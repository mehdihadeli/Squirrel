using Squirrel.Abstractions.Commands;
using Squirrel.Abstractions.Web.MinimalApi;
using Microsoft.AspNetCore.Http;

namespace Squirrel.Web.Minimal;

public record HttpCommand<TRequest>(
    TRequest Request,
    HttpContext HttpContext,
    ICommandBus CommandBus,
    CancellationToken CancellationToken
) : IHttpCommand<TRequest>;
