using Microsoft.EntityFrameworkCore;

namespace Squirrel.Abstractions.Persistence;

public interface IDataSeeder<in TContext>
    where TContext : DbContext
{
    Task SeedAsync(TContext context);
}
