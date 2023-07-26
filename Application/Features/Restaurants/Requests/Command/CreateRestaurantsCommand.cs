using Application.Dtos;
using Application.Response;
using MediatR;

namespace Application.Features.Restaurants.Requests.Command
{
    public class CreateRestaurantsCommand : IRequest<ApiResponse>
    {
        public List<RestaurantDto> RestaurantsDto { get; set; }
    }
}
