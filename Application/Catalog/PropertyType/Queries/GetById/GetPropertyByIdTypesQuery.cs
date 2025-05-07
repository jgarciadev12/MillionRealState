using Application.Common;
using Application.DTOs;
using MediatR;

namespace Application.Catalog.PropertyType.Queries.Get
{
    public class GetPropertyByIdTypesQuery : IRequest<PropertyDto?>
    {
        public string Id { get; set;}
    }
}
