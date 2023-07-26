using Application.Features.ShoppingCart.Requests.Queries;
using Core.Interfaces;
using MediatR;
using CartEntity = Core.Entities.ShoppingCart;

namespace Application.Features.ShoppingCart.Handlers.Queries
{
    public class GetShoppingCartRequestHandler : IRequestHandler<GetShoppingCartRequest, CartEntity>
    {
        private readonly IShoppingCartRepository _shoppingCart;

        public GetShoppingCartRequestHandler(IShoppingCartRepository shoppingCart)
        {
            _shoppingCart = shoppingCart;
        }

        public Task<CartEntity> Handle(GetShoppingCartRequest request, CancellationToken cancellationToken)
        {
            return _shoppingCart.GetShoppingCartAsync(request.ShoppingCartId);
        }
    }
}
