using Application.Common;
using Application.DTOs;
using Application.Repository.PropertyRepository;
using AutoMapper;
using MediatR;

namespace Application.Catalog.PropertyType.Queries.Get
{
    public class GetPropertiesByFilterQueryHandler : IRequestHandler<GetPropertiesByFilterQuery, PaginatedResult<PropertyDto>>
    {
        private readonly IPropertyRepository propertyRepository;
        private readonly IMapper _mapper;
        public GetPropertiesByFilterQueryHandler(IPropertyRepository propertyRepository, IMapper mapper)
        {
            this.propertyRepository = propertyRepository;
            this._mapper = mapper;
        }
        public async Task<PaginatedResult<PropertyDto>> Handle(GetPropertiesByFilterQuery request, CancellationToken cancellationToken)
        {
            var repositoryResponse = await propertyRepository.GetByFilterAsync(request.name, request.address, request.minPrice, request.maxPrice);
            var total = repositoryResponse.Count();

            var items = repositoryResponse
                .Skip((request.Page - 1) * request.PageSize)
                .Take(request.PageSize);

            var result = new PaginatedResult<PropertyDto>
            {
                Items = items.Select(p => _mapper.Map<PropertyDto>(p)).ToList(),
                TotalCount = (int)total
            };

            return result;
        }
    }
}
