using AutoMapper;
using Talabat.APIs.DTOs;
using Talabat.Core.Entities;
using Talabat.Core.Entities.Identity;

namespace Talabat.APIs.Helpers
{
    public class MappingProfiles : Profile
    {
        public MappingProfiles()
        {
            CreateMap<Product, ProductToReturnDTO>()
                .ForMember(D => D.ProductType, O => O.MapFrom(S => S.ProductType.Name))
                .ForMember(D => D.ProductBrand, O => O.MapFrom(S => S.ProductBrand.Name))
                .ForMember(D => D.PictureUrl, O => O.MapFrom<ProductPictureUrlResolver>());
            CreateMap<AdressDTo,Talabat.Core.Entities.Identity.Address>().ReverseMap();

            CreateMap<CustomerBasketDTO , CustomerBasket>();
            CreateMap<BasketItemDTO , BasketItem>();

            CreateMap<AdressDTo, Talabat.Core.Entities.Orders_Aggrigate.Address>();

        }

    }
}
