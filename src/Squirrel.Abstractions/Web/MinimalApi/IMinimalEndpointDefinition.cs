using Microsoft.AspNetCore.Routing;

namespace Squirrel.Abstractions.Web.MinimalApi;

public interface IMinimalEndpointDefinition
{
    IEndpointRouteBuilder MapEndpoint(IEndpointRouteBuilder builder);
}