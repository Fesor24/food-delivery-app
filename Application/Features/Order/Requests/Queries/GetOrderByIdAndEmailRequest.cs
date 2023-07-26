using Application.Dtos;
using MediatR;

namespace Application.Features.Order.Requests.Queries
{
    public class GetOrderByIdAndEmailRequest : IRequest<OrderToReturnDto>
    {
        public string Email { get; set; }

        public string OrderId { get; set; }
    }
}
