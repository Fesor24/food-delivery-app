using API.Dtos;
using API.Helpers;
using API.Response;
using AutoMapper;
using Core.Entities;
using Core.Interfaces;
using Core.Specifications;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    
    [ApiController]
    public class RestaurantController : ControllerBase
    {
        private readonly IGenericRepository<Restaurant> _restaurantRepo;

        private readonly IMapper _mapper;

        public RestaurantController(IGenericRepository<Restaurant> restaurantRepo, IMapper mapper)
        {
            _restaurantRepo= restaurantRepo;
            _mapper = mapper;
        }

        [HttpPost(Routes.AddRestaurants)]
        public async Task AddRestaurants(List<RestaurantDto> restaurantDto)
        {
            var restaurant = _mapper.Map<List<RestaurantDto>, List<Restaurant>>(restaurantDto);

            await _restaurantRepo.AddListAsync(restaurant);
        }

        [HttpGet(Routes.FetchRestaurants)]
        public async Task<ApiResponse> FetchRestaurants([FromQuery] string sort, [FromQuery] string location)
        {
            var spec = new RestaurantSpecification(sort, location);

            var restaurant = await _restaurantRepo.GetAllWIthSpecAsync(spec);

            var restaurantToReturn = _mapper.Map<IReadOnlyList<Restaurant>, IReadOnlyList<RestaurantDto>>(restaurant);

            return new ApiResponse
            {
                Result = restaurantToReturn
            };
        }
    }
}
