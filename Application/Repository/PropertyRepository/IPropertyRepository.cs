using Application.Repository.ICommon;
using Domain.Entities;

namespace Application.Repository.PropertyRepository
{
    public interface IPropertyRepository : IBaseRepository<Property>
    {
        public Task<IReadOnlyList<Property>> GetByFilterAsync(string? name, string? address, decimal? minPrice, decimal? maxPrice);
    }
}
