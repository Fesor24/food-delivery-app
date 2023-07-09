using API.Dtos;
using AutoMapper;
using Core.Entities;

namespace API.Helpers
{
    public class ProductImageResolver : IValueResolver<Products, ProductDto, string>
    {
        private readonly IConfiguration _config;
        public ProductImageResolver(IConfiguration config)
        {
            _config= config;
        }

        public string Resolve(Products source, ProductDto destination, string destMember, ResolutionContext context)
        {
            if (!string.IsNullOrEmpty(source.PictureUrl))
            {
                return _config["Host"] + source.PictureUrl;
            }

            return null;
        }
    }
}
