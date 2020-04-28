using API.Dtos;
using AutoMapper;
using Core.Entities;

namespace API.Helpers
{
    public class MappingProfiles : Profile
    {
        public MappingProfiles()
        {
            CreateMap<Product, ProductToReturnDto>()
                .ForMember(x => x.ProductBrand, opt => opt.MapFrom(z => z.ProductBrand != null ? z.ProductBrand.Name : null))
                .ForMember(x => x.ProductType, opt => opt.MapFrom(z => z.ProductType != null ? z.ProductType.Name : null))
                .ReverseMap();
        }
    }
}