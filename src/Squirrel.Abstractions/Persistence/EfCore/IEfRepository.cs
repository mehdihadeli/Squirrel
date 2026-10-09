using System.Linq.Expressions;
using Squirrel.Abstractions.Domain;
using Microsoft.EntityFrameworkCore.Query;

namespace Squirrel.Abstractions.Persistence.EfCore;

public interface IEfRepository<TEntity, in TId> : IRepository<TEntity, TId>
    where TEntity : class, IEntity<TId>
{
    IEnumerable<TEntity> GetInclude(Func<IQueryable<TEntity>, IIncludableQueryable<TEntity, object>>? includes = null);

    IEnumerable<TEntity> GetInclude(
        Expression<Func<TEntity, bool>> predicate,
        Func<IQueryable<TEntity>, IIncludableQueryable<TEntity, object>>? includes = null,
        bool withTracking = true
    );

    Task<IEnumerable<TEntity>> GetIncludeAsync(
        Func<IQueryable<TEntity>, IIncludableQueryable<TEntity, object>>? includes = null
    );

    Task<IEnumerable<TEntity>> GetIncludeAsync(
        Expression<Func<TEntity, bool>> predicate,
        Func<IQueryable<TEntity>, IIncludableQueryable<TEntity, object>>? includes = null,
        bool withTracking = true
    );
}
