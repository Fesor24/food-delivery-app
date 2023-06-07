using API.Dtos;
using AutoMapper;
using Core.Entities;

namespace API.Helpers
{
    public class MappingProfiles : Profile
    {
        public MappingProfiles()
        {
            CreateMap<Restaurant, RestaurantDto>()
                .ForMember(x => x.PictureUrl, o => o.MapFrom<RestaurantImageResolver>())
                .ReverseMap();
        }
    }
}
