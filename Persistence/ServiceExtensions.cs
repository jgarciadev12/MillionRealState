using Application.Repository.PropertyRepository;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using MongoDB.Driver;
using Persistence.Repositories;
using Persistence.Settings;

namespace Persistence
{
    public static class ServiceExtensions
    {
        public static void ConfigurePersistence(this IServiceCollection services, IConfiguration configuration)
        {
            var section = configuration.GetSection("MongoDB");

            var mongoConfig = section.Get<MongoSettings>();
            var mongoClient = new MongoClient(mongoConfig.ConnectionString);
            var mongoDatabase = mongoClient.GetDatabase(mongoConfig.DatabaseName);

            services.AddSingleton(mongoDatabase);
            services.AddScoped<IPropertyRepository, PropertyRepository>();
        }
    }
}
