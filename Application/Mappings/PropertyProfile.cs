using Application.DTOs;
using AutoMapper;
using Domain.Entities;

namespace Application.Mappings
{
    public class PropertyProfile : Profile
    {
        public PropertyProfile()
        {
            CreateMap<Property, PropertyDto>()
            .ForMember(dest => dest.ImageUrl, opt =>
                opt.MapFrom(src => src.Images != null && src.Images.Any()
                    ? src.Images.First().File 
                    : null));

            CreateMap<PropertyDto, Property>();

        }
    }
}
