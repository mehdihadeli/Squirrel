using Squirrel.Abstractions.Domain;

namespace Squirrel.Abstractions.Persistence.Mongo;

public interface IMongoRepository<TEntity, in TId> : IRepository<TEntity, TId>
    where TEntity : class, IEntity<TId>;
