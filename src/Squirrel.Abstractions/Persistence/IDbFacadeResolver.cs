using Microsoft.EntityFrameworkCore.Infrastructure;

namespace Squirrel.Abstractions.Persistence;

public interface IDbFacadeResolver
{
    DatabaseFacade Database { get; }
}
