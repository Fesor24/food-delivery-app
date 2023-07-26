using System.Net;
using API.Helpers;
using Application.Dtos;
using Application.Features.Restaurants.Requests.Command;
using Application.Features.Restaurants.Requests.Queries;
using Application.Response;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{

    [ApiController]
    public class RestaurantController : ControllerBase
    {
        private readonly IMediator _mediator;

        public RestaurantController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost(Routes.AddRestaurants)]
        public async Task<ApiResponse> AddRestaurants(List<RestaurantDto> restaurantDto)
        {
            var response = await _mediator.Send(new CreateRestaurantsCommand { RestaurantsDto = restaurantDto });

            if (response.Successful)
            {
                Response.StatusCode = 201;
            }

            return response;
        }

        [HttpGet(Routes.FetchRestaurants)]
        public async Task<ApiResponse> FetchRestaurants([FromQuery] string sort, [FromQuery] string location)
        {
            var restuarants = await _mediator.Send(new GetRestaurantsRequest { Location = location, Sort = sort });

            return new ApiResponse
            {
                Result = restuarants
            };
        }

        [HttpGet(Routes.FetchRestaurant)]
        public async Task<ApiResponse> FetchRestaurant([FromQuery] string restaurantId)
        {
            var restaurant = await _mediator.Send(new GetRestaurantByIdRequest(restaurantId));

            if(restaurant is null)
            {
                Response.StatusCode = (int)HttpStatusCode.NotFound;

                return new ApiResponse
                {
                    ErrorMessage = "Restaurant not found"
                };
            }

            return new ApiResponse
            {
                Result = restaurant
            };
        }
    }
}
