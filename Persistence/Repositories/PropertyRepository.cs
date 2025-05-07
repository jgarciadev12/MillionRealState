using Application.Repository.PropertyRepository;
using Domain.Entities;
using MongoDB.Driver;

namespace Persistence.Repositories
{
    public class PropertyRepository : BaseRepository<Property>, IPropertyRepository
    {
        public PropertyRepository(IMongoDatabase database) : base(database, "Properties") { }

        public Task<IReadOnlyList<Property>> GetByFilterAsync(string? name, string? address, decimal? minPrice, decimal? maxPrice)
        {
            var filterBuilder = Builders<Property>.Filter;
            var filters = new List<FilterDefinition<Property>>();

            if (!string.IsNullOrEmpty(name))
                filters.Add(filterBuilder.Regex(p => p.Name, new MongoDB.Bson.BsonRegularExpression(name, "i")));
            if (!string.IsNullOrEmpty(address))
                filters.Add(filterBuilder.Regex(p => p.Address, new MongoDB.Bson.BsonRegularExpression(address, "i")));
            if (minPrice.HasValue)
                filters.Add(filterBuilder.Gte(p => p.Price, minPrice.Value));
            if (maxPrice.HasValue)
                filters.Add(filterBuilder.Lte(p => p.Price, maxPrice.Value));

            var combinedFilter = filters.Any() ? filterBuilder.And(filters) : filterBuilder.Empty;
            return GetByFilterAsync(combinedFilter);
        }
    }
}
