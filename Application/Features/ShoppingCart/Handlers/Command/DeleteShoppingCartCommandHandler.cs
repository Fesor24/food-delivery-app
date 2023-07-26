using Application.Features.ShoppingCart.Requests.Command;
using Core.Interfaces;
using MediatR;

namespace Application.Features.ShoppingCart.Handlers.Command
{
    public class DeleteShoppingCartCommandHandler : IRequestHandler<DeleteShoppingCartCommand, Unit>
    {
        private readonly IShoppingCartRepository _shoppingCart;

        public DeleteShoppingCartCommandHandler(IShoppingCartRepository shoppingCart)
        {
            _shoppingCart = shoppingCart;
        }

        public async Task<Unit> Handle(DeleteShoppingCartCommand request, CancellationToken cancellationToken)
        {
            await _shoppingCart.DeleteShoppingCartAsync(request.ShoppingCartId);

            return Unit.Value;
        }
    }
}
