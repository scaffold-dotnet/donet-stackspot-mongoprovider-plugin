using MongoDB.Driver;
using System;

namespace MongoProvider
{
    public class MongoContext : IMongoAccess
    {
        private readonly MongoConfig _mongoConfig;

        private readonly Lazy<IMongoClient> client;

        private readonly Lazy<IMongoDatabase> database;

        public MongoContext(MongoConfig mongoConfig)
        {
            _mongoConfig = mongoConfig;

            client = new Lazy<IMongoClient>(() => GetMongoClient());
            database = new Lazy<IMongoDatabase>(() => GetMongoDatabase());
        }

        public IMongoDatabase Database => database.Value;
        public IMongoClient Client => client.Value;

        private MongoClient GetMongoClient()
        {
            return new MongoClient($"{_mongoConfig.Conexao}/{_mongoConfig.Banco}");
        }

        private IMongoDatabase GetMongoDatabase()
        {
            return Client.GetDatabase(_mongoConfig.Banco);
        }

        public IMongoCollection<T> GetCollection<T>(string name)
        {
            return Database.GetCollection<T>(name);
        }
    }
}
