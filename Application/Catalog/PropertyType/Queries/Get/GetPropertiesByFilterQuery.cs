using Application.Common;
using Application.DTOs;
using MediatR;

namespace Application.Catalog.PropertyType.Queries.Get
{
    public class GetPropertiesByFilterQuery : IRequest<PaginatedResult<PropertyDto>>
    {
        public string? name { get; set; }
        public string? address { get; set; }
        public decimal? minPrice { get; set; } 
        public decimal? maxPrice { get; set; }
        public int Page { get; set; } = 1;
        public int PageSize { get; set; } = 6;
    }
}
