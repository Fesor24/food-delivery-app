using System.Net;
using API.Dtos;
using API.Helpers;
using API.Response;
using AutoMapper;
using Core.Entities;
using Core.Interfaces;
using Core.Specifications;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{

    [ApiController]
    public class RestaurantController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;

        private readonly IMapper _mapper;

        public RestaurantController(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        [HttpPost(Routes.AddRestaurants)]
        public async Task AddRestaurants(List<RestaurantDto> restaurantDto)
        {
            var restaurant = _mapper.Map<List<RestaurantDto>, List<Restaurant>>(restaurantDto);

            await _unitOfWork.Repository<Restaurant>().AddListAsync(restaurant);
        }

        [HttpGet(Routes.FetchRestaurants)]
        public async Task<ApiResponse> FetchRestaurants([FromQuery] string sort, [FromQuery] string location)
        {
            var spec = new RestaurantSpecification(sort, location);

            var restaurant = await _unitOfWork.Repository<Restaurant>().GetAllWIthSpecAsync(spec);

            var restaurantToReturn = _mapper.Map<IReadOnlyList<Restaurant>, IReadOnlyList<RestaurantDto>>(restaurant);

            return new ApiResponse
            {
                Result = restaurantToReturn
            };
        }

        [HttpGet(Routes.FetchRestaurant)]
        public async Task<ApiResponse> FetchRestaurant([FromQuery] string restaurantId)
        {
            var spec = new RestaurantByIdSpecification(restaurantId);

            var restaurant = await _unitOfWork.Repository<Restaurant>().GetWithSpecAsync(spec);

            if(restaurant is null)
            {
                Response.StatusCode = (int)HttpStatusCode.NotFound;

                return new ApiResponse
                {
                    ErrorMessage = "Restaurant not found"
                };
            }

            var restaurantToReturn = _mapper.Map<Restaurant, RestaurantDto>(restaurant);

            return new ApiResponse
            {
                Result = restaurantToReturn
            };
        }
    }
}
