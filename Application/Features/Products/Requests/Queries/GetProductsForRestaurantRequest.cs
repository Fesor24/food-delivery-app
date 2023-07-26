using Application.Dtos;
using MediatR;

namespace Application.Features.Products.Requests.Queries
{
    public class GetProductsForRestaurantRequest : IRequest<IReadOnlyList<ProductDto>>
    {
        public string RestaurantId { get; set; }
    }
}
