using MongoDB.Driver;
using ScaffoldDotnet.MongoProvider.Attributes;
using ScaffoldDotnet.MongoProvider.Cache;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading;
using System.Threading.Tasks;

namespace ScaffoldDotnet.MongoProvider
{
    public class MongoMethods<T> : IMongoMethods<T> where T : MongoEntity
    {
        private readonly IMongoCollection<T> collection;

        public MongoMethods(IMongoAccess mongoAccess)
        {
            collection = mongoAccess.GetCollection<T>(CollectionName);
        }

        protected IMongoCollection<T> Collection { get { return collection; } }

        public string CollectionName
        {
            get
            {
                var type = typeof(T);

                if (AttributeCache.TryGet(type.Name, out string value))
                {
                    return value;
                }

                var attribute = type
                                 .GetCustomAttributes(false)
                                 .Where(c => c is DocumentName)
                                 .Cast<DocumentName>()
                                 .FirstOrDefault();

                AttributeCache.Add(type.Name, attribute.Name);

                return attribute.Name;
            }
        }

        public Task InsertOneAsync(T document, CancellationToken cancellationToken = default)
        {
            return collection.InsertOneAsync(document, null, cancellationToken);
        }

        public Task<T> FindAsync(Expression<Func<T, bool>> filter, CancellationToken cancellationToken = default)
        {
            return collection
                .FindSync<T>(filter, null, cancellationToken)
                .FirstOrDefaultAsync();
        }

        public Task<List<T>> FindAllAsync(Expression<Func<T, bool>> filter, CancellationToken cancellationToken = default)
        {
            return collection
                .FindSync<T>(filter, null, cancellationToken)
                .ToListAsync();
        }

        public async Task ReplaceOneAsync(Expression<Func<T, bool>> filter, T data, CancellationToken cancellationToken = default)
        {
            var persistedData = await FindAsync(filter);
            if (persistedData != null)
            {
                await collection.ReplaceOneAsync(filter, data, default(ReplaceOptions), cancellationToken);
            }
        }

        public Task DeleteOneAsync(Expression<Func<T, bool>> filter, CancellationToken cancellationToken = default)
        {
            return collection.DeleteOneAsync(filter, null, cancellationToken);
        }

        public Task InsertManyAsync(IEnumerable<T> documents, CancellationToken cancellationToken = default)
        {
            return collection.InsertManyAsync(documents);
        }
    }
}
