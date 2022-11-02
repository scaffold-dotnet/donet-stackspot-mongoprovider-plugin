using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace MongoProvider
{
    public static class DependencyInjection
    {
        public static void AddMongoProviders(this IServiceCollection services, IConfiguration configuration)
        {
            var mongoConfig = new MongoConfig();
            configuration.GetSection("Mongo").Bind(mongoConfig);
            services.AddSingleton(mongoConfig);

            services.AddScoped(typeof(IMongoMethods<>), typeof(MongoMethods<>));
            services.AddScoped<IMongoAccess, MongoContext>();
        }
    }
}
