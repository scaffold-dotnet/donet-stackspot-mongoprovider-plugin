using MongoDB.Driver;
using MongoProvider.Attributes;
using MongoProvider.Cache;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace MongoProvider
{
    public class MongoMethods<T> : IMongoMethods<T> where T : MongoEntity
    {
        private readonly IMongoCollection<T> collection;

        public MongoMethods(IMongoAccess mongoAccess)
        {
            this.collection = mongoAccess.GetCollection<T>(CollectionName);
        }

        protected IMongoCollection<T> Collection { get { return this.collection; } }

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

        public async Task InsertOneAsync(T document)
        {
            await collection.InsertOneAsync(document);
        }

        public async Task<T> FindAsync(Expression<Func<T, bool>> filter)
        {
            return await collection.FindSync<T>(filter).FirstOrDefaultAsync();
        }

        public async Task<IEnumerable<T>> FindAllAsync(Expression<Func<T, bool>> filter)
        {
            return await collection.Find(filter).ToListAsync();
        }

        public async Task ReplaceOneAsync(Expression<Func<T, bool>> filter, T data)
        {
            var persistedData = await FindAsync(filter);
            if (persistedData != null)
            {
                await collection.ReplaceOneAsync(filter, data);
            }
        }

        public async Task DeleteOneAsync(Expression<Func<T, bool>> filter)
        {
            await collection.DeleteOneAsync(filter);
        }

        public async Task InsertManyAsync(IEnumerable<T> documents)
        {
            await collection.InsertManyAsync(documents);
        }
    }

}
