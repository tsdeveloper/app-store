using API.Dtos;
using API.Helpers.Events;
using API.Helpers.Products;
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
                .ForMember(x => x.PictureUrl, opt => opt.MapFrom<ProductUrlResolver>())
                .ReverseMap();
            
            CreateMap<Client, ClientToReturnDto>()
                .ReverseMap();
            
            CreateMap<Event, EventToReturnDto>()
                .ForMember(x => x.ClientName, opt => opt.MapFrom<EventClientNameResolver>())
                .ReverseMap();
            
            
            CreateMap<Ticket, TicketToReturnDto>()
                .ReverseMap();
        }
    }
}