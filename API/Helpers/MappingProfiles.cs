using API.Dtos;
using AutoMapper;
using Core.Entities;
using Core.Entities.Identity;

namespace API.Helpers
{
    public class MappingProfiles : Profile
    {
        public MappingProfiles()
        {
            CreateMap<Product,ProductToReturnDto>()
                .ForMember(dest => dest.ProductBrand, opts => opts.MapFrom(src => src.ProductBrand.Name))
                .ForMember(dest => dest.ProductType, opts => opts.MapFrom(src => src.ProductType.Name))
                .ForMember(dest => dest.PictureUrl, opts => opts.MapFrom<ProductUrlResolver>());

            CreateMap<Address,AddressDto>().ReverseMap();
        }
    }
}