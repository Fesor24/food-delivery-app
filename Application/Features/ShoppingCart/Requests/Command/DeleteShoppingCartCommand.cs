using MediatR;

namespace Application.Features.ShoppingCart.Requests.Command
{
    public class DeleteShoppingCartCommand : IRequest<Unit>
    {
        public string ShoppingCartId { get; set; }
    }
}
