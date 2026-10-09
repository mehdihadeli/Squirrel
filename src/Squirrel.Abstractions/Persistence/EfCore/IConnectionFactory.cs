using System.Data.Common;

namespace Squirrel.Abstractions.Persistence.EfCore;

public interface IConnectionFactory : IDisposable
{
    Task<DbConnection> GetOrCreateConnectionAsync();
}
