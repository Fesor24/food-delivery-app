using Application.Dtos;
using MediatR;

namespace Application.Features.Order.Requests.Commands
{
    public class VerifyPaymentCommand : IRequest<OrderToReturnDto>
    {
        public string Trxref { get; set; }
    }
}
