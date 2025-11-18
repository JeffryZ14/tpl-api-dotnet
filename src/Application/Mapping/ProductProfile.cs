using Application.Products.Dtos;
using AutoMapper;
using Domain.Entities;

namespace Application.Mapping
{
    public class ProductProfile : Profile
    {
        public ProductProfile()
        {
            CreateMap<Product, ProductDto>()
            .ForMember(d => d.Id,
                       opt => opt.MapFrom(src => src.Id.Value))
            .ForMember(d => d.Name,
                       opt => opt.MapFrom(src => src.Name.Value))
            .ForMember(d => d.Price,
                       opt => opt.MapFrom(src => src.Price.Amount))
            .ForMember(d => d.Currency,
                       opt => opt.MapFrom(src => src.Price.Currency));
        }
    }
}
