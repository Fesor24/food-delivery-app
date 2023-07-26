using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Application.Dtos;
using Application.Features.Order.Requests.Queries;
using AutoMapper;
using OrderEntity = Core.Entities.OrderAggregate.Order;
using Core.Interfaces;
using MediatR;

namespace Application.Features.Order.Handlers.Queries
{
    public class GetOrdersForUserRequestHandler : IRequestHandler<GetOrdersForUserRequest, IReadOnlyList<OrderToReturnDto>>
    {
        private readonly IMapper _mapper;

        private readonly IOrderService _orderService;

        public GetOrdersForUserRequestHandler(IMapper mapper, IOrderService orderService)
        {
            _mapper = mapper;
            _orderService = orderService;
        }

        public async Task<IReadOnlyList<OrderToReturnDto>> Handle(GetOrdersForUserRequest request, CancellationToken cancellationToken)
        {
            var orders = await _orderService.GetOrdersForUserAsync(request.Email);

            var orderToReturn = _mapper.Map<IReadOnlyList<OrderEntity>, IReadOnlyList<OrderToReturnDto>>(orders);

            return orderToReturn;
        }
    }
}
