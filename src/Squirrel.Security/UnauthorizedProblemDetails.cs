using Microsoft.AspNetCore.Mvc;

namespace Squirrel.Security;

public class UnauthorizedProblemDetails : ProblemDetails
{
    public UnauthorizedProblemDetails(string? details = null)
    {
        Title = "UnauthorizedException";
        Detail = details;
        Status = 401;
    }
}
