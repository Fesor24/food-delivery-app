using Application.Dtos;
using MediatR;

namespace Application.Features.Restaurants.Requests.Queries
{
    public class GetRestaurantByIdRequest : IRequest<RestaurantDto>
    {
        public GetRestaurantByIdRequest(string id)
        {
            Id = id;
        }

        public string Id { get; set; }
    }
}
