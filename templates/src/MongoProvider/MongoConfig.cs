namespace MongoProvider
{
    public class MongoConfig
    {
        public MongoConfig(string connection)
        {
            Connection = connection;
        }

        public string Connection { get; }
    }
}
