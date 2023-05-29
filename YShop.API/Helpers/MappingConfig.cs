using AutoMapper;
using YShop.core.Models;
using YShop.core.ViewModels;

namespace YShop.API.Helpers
{
    public class MappingConfig : Profile
    {
        public MappingConfig() 
        {
            CreateMap<Product, ProductDto>()
                .ForMember(d=>d.ProductBrand , o=>o.MapFrom(s=>s.ProductBrand.BrandName))
                .ForMember(d=>d.ProductType , o=>o.MapFrom(s=>s.ProductType.TypeName))
                .ForMember(d=>d.PictureUrl , o=>o.MapFrom<ProductUrlResolver>());
        }
    }
}
