using Microsoft.Extensions.DependencyInjection;

namespace MongoProvider
{
    public static class Inicializer
    {
        public static void AddMongoProviders(this IServiceCollection services, string connection)
        {
            var mongoConfig = new MongoConfig(connection);
            services.AddSingleton(mongoConfig);

            services.AddScoped(typeof(IMongoMethods<>), typeof(MongoMethods<>));
            services.AddScoped<IMongoAccess, MongoContext>();
        }
    }
}
