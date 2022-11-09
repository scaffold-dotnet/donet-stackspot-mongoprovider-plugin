using MongoDB.Driver;

namespace ScaffoldDotnet.MongoProvider
{
    public interface IMongoAccess
    {
        IMongoCollection<T> GetCollection<T>(string name);
    }

}
