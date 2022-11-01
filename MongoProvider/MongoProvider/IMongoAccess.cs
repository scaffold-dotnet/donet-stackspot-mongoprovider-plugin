using MongoDB.Driver;

namespace MongoProvider
{
    public interface IMongoAccess
    {
        IMongoCollection<T> GetCollection<T>(string name);
    }

}
