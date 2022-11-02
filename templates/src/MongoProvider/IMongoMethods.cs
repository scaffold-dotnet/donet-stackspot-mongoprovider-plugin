using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading;
using System.Threading.Tasks;

namespace MongoProvider
{
    public interface IMongoMethods<TEntity>
    {
        public string CollectionName { get; }

        Task InsertOneAsync(TEntity document, CancellationToken cancellationToken = default);
        Task<TEntity> FindAsync(Expression<Func<TEntity, bool>> filter, CancellationToken cancellationToken = default);
        Task<List<TEntity>> FindAllAsync(Expression<Func<TEntity, bool>> filter, CancellationToken cancellationToken = default);
        Task ReplaceOneAsync(Expression<Func<TEntity, bool>> filter, TEntity data, CancellationToken cancellationToken = default);
        Task DeleteOneAsync(Expression<Func<TEntity, bool>> filter, CancellationToken cancellationToken = default);
        Task InsertManyAsync(IEnumerable<TEntity> documents, CancellationToken cancellationToken = default);
    }

}
