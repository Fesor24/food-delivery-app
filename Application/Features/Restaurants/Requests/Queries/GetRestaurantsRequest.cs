using Application.Dtos;
using MediatR;

namespace Application.Features.Restaurants.Requests.Queries
{
    public class GetRestaurantsRequest : IRequest<IReadOnlyList<RestaurantDto>>
    {
        public string Sort { get; set; }

        public string Location { get; set; }
    }
}
