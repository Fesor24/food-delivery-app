using Application.Dtos;
using MediatR;

namespace Application.Features.Order.Requests.Queries
{
    public class GetOrdersForUserRequest : IRequest<IReadOnlyList<OrderToReturnDto>>
    {
        public string Email { get; set; }
    }
}
