using MediatR;
using CartEntity = Core.Entities.ShoppingCart;

namespace Application.Features.ShoppingCart.Requests.Queries
{
    public class GetShoppingCartRequest : IRequest<CartEntity>
    {
        public string ShoppingCartId { get; set; }
    }
}
