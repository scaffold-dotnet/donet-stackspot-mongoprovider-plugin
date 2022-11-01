using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace MongoProvider
{
    public interface IMongoMethods<TEntity>
    {
        public string CollectionName { get; }

        Task InsertOneAsync(TEntity document);
        Task<TEntity> FindAsync(Expression<Func<TEntity, bool>> filter);
        Task<IEnumerable<TEntity>> FindAllAsync(Expression<Func<TEntity, bool>> filter);
        Task ReplaceOneAsync(Expression<Func<TEntity, bool>> filter, TEntity data);
        Task DeleteOneAsync(Expression<Func<TEntity, bool>> filter);
        Task InsertManyAsync(IEnumerable<TEntity> documents);
    }

}
