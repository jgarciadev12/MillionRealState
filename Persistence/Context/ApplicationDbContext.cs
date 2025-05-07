using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using MongoDB.Driver;
using Persistence.Settings;

namespace Persistence.Context
{
    public class ApplicationDbContext : DbContext
    {
        private readonly IMongoDatabase _database;

        public ApplicationDbContext(MongoSettings settings)
        {
            var client = new MongoClient(settings.ConnectionString);
            _database = client.GetDatabase(settings.DatabaseName);
        }

        public IMongoCollection<Owner> Owners => _database.GetCollection<Owner>("Owners");
        public IMongoCollection<Property> Properties => _database.GetCollection<Property>("Properties");

    }
}
