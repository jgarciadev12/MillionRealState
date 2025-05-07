using Application.Catalog.PropertyType.Queries.Get;
using Application.Common;
using Application.Common.Exceptions;
using Application.DTOs;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Web.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PropertyController : ControllerBase
    {
        private readonly IMediator _mediator;

        public PropertyController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        [Produces("application/json")]
        public async Task<PaginatedResult<PropertyDto>> GetFilterProperties([FromQuery] string? name, [FromQuery] string? address,
            [FromQuery] decimal? minPrice, [FromQuery] decimal? maxPrice, [FromQuery] int page = 1,
            [FromQuery] int pageSize = 6)
        {
            var query = new GetPropertiesByFilterQuery
            {
                name = name,
                address = address,
                minPrice = minPrice,
                maxPrice = maxPrice,
                Page = page,
                PageSize = pageSize
            };
            return await _mediator.Send(query);
        }

        [HttpGet("{id:length(24)}")]
        public async Task<IActionResult> GetById(string id)
        {
            var result = await _mediator.Send(new GetPropertyByIdTypesQuery { Id = id });
            if (result == null) throw new NoDataFoundException($"Property with ID {id} not found"); ;
            return Ok(result);
        }
    }
}
