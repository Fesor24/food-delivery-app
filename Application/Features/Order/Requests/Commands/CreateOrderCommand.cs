using Application.Dtos;
using MediatR;

namespace Application.Features.Order.Requests.Commands
{
    public class CreateOrderCommand : IRequest<string>
    {
        public OrderDto Order { get; set; }

        public string Email { get; set; }
    }
}
