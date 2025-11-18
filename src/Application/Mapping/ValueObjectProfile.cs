using AutoMapper;
using Domain.ValueObjects;

namespace Application.Mapping
{
    public class ValueObjectProfile : Profile
    {
        public ValueObjectProfile()
        {
            CreateMap<ProductName, string>()
                .ConvertUsing(src => src.Value);

            CreateMap<Money, decimal>()
                .ConvertUsing(src => src.Amount);

            CreateMap<Money, string>()
                .ConvertUsing(src => src.Currency);
        }
    }
}