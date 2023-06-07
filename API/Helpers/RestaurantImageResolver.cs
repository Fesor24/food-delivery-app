using API.Dtos;
using AutoMapper;
using Core.Entities;

namespace API.Helpers
{
    public class RestaurantImageResolver : IValueResolver<Restaurant, RestaurantDto, string>
    {
        private readonly IConfiguration _config;

        public RestaurantImageResolver(IConfiguration config)
        {
            _config= config;
        }

        public string Resolve(Restaurant source, RestaurantDto destination, string destMember, ResolutionContext context)
        {
            if (!string.IsNullOrEmpty(source.PictureUrl))
            {
                return _config["Host"] + source.PictureUrl;
            }

            return null;
        }
    }
}
