using API.Dtos;
using AutoMapper;
using Core.Entities;
using Core.Entities.OrderAggregate;

namespace API.Helpers
{
    public class MappingProfiles : Profile
    {
        public MappingProfiles()
        {
            CreateMap<Restaurant, RestaurantDto>()
                .ForMember(x => x.PictureUrl, o => o.MapFrom<RestaurantImageResolver>())
                .ReverseMap();

            CreateMap<Products, ProductDto>()
                .ForMember(x => x.PictureUrl, o => o.MapFrom<ProductImageResolver>())
                .ReverseMap();

            CreateMap<Products, CreateProductDto>()
                .ReverseMap();

            CreateMap<ShoppingCartItemDto, ShoppingCartItem>();

            CreateMap<ShoppingCartDto, ShoppingCart>();

            CreateMap<AddressDto, Address>();

            CreateMap<Order, OrderToReturnDto>()
                .ForMember(x => x.DateCreated, c => c.MapFrom(x => x.DateCreated.ToString("ddd dd MMM yyyy")));

            CreateMap<OrderItem, OrderItemDto>()
                .ForMember(x => x.ProductId, c => c.MapFrom(x => x.ItemOrdered.ProductItemId))
                .ForMember(x => x.ProductName, c => c.MapFrom(x => x.ItemOrdered.ProductName))
                .ForMember(x => x.PictureUrl, c => c.MapFrom<OrderImageResolver>());
        }
    }
}
