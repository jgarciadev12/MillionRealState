using Application.Catalog.PropertyType.Queries.Get;
using Application.DTOs;
using Application.Repository.PropertyRepository;
using AutoMapper;
using MediatR;

namespace Application.Catalog.PropertyType.Queries.GetById
{
    public class GetPropertyByIdQueryHandler : IRequestHandler<GetPropertyByIdTypesQuery, PropertyDto?>
    {
        private readonly IPropertyRepository _repository;
        private readonly IMapper _mapper;
        public GetPropertyByIdQueryHandler(IPropertyRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<PropertyDto?> Handle(GetPropertyByIdTypesQuery request, CancellationToken cancellationToken)
        {
            var property = await _repository.GetByIdAsync(request.Id);
            return property == null ? null : _mapper.Map<PropertyDto>(property);
        }
    }
}
