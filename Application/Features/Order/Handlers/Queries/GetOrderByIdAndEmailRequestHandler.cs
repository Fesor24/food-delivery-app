using Application.Dtos;
using Application.Features.Order.Requests.Queries;
using AutoMapper;
using Core.Interfaces;
using MediatR;
using OrderEntity = Core.Entities.OrderAggregate.Order;

namespace Application.Features.Order.Handlers.Queries
{
    public class GetOrderByIdAndEmailRequestHandler : IRequestHandler<GetOrderByIdAndEmailRequest, OrderToReturnDto>
    {
        private readonly IOrderService _orderService;

        private readonly IMapper _mapper;

        public GetOrderByIdAndEmailRequestHandler(IOrderService orderService, IMapper mapper)
        {
            _orderService = orderService;
            _mapper = mapper;
        }

        public async Task<OrderToReturnDto> Handle(GetOrderByIdAndEmailRequest request, CancellationToken cancellationToken)
        {
            var order = await _orderService.GetOrderByIdAndEmailAsync(request.Email, request.OrderId);

            if(order is null)
            {
                return null;
            }

            var orderToReturnDto = _mapper.Map<OrderEntity, OrderToReturnDto>(order);

            return orderToReturnDto;
        }
    }
}
