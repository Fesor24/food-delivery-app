using Application.Dtos;
using CartEntity = Core.Entities.ShoppingCart;
using MediatR;

namespace Application.Features.ShoppingCart.Requests.Command
{
    public class UpdateShoppingCartCommand : IRequest<CartEntity>
    {
        public ShoppingCartDto ShoppingCart { get; set; }
    }
}
