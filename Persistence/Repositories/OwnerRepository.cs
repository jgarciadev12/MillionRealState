using Application.Repository.OwnerRepository;
using Domain.Entities;
using MongoDB.Driver;

namespace Persistence.Repositories
{
    public class OwnerRepository : BaseRepository<Owner>, IOwnerRepository
    {
        public OwnerRepository(IMongoDatabase database)
            : base(database, "Owners") { }
    }
}
