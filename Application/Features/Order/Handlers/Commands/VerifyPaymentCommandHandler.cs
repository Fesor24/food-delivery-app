using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Application.Dtos;
using Application.Features.Order.Requests.Commands;
using AutoMapper;
using OrderEntity =  Core.Entities.OrderAggregate.Order;
using Core.Interfaces;
using MediatR;
using PayStack.Net;

namespace Application.Features.Order.Handlers.Commands
{
    public class VerifyPaymentCommandHandler : IRequestHandler<VerifyPaymentCommand, OrderToReturnDto>
    {
        private readonly IOrderService _orderService;

        private readonly IMapper _mapper;

        private readonly IPaymentService<TransactionInitializeResponse, TransactionVerifyResponse, object> _payStack;

        public VerifyPaymentCommandHandler(IOrderService orderService, IMapper mapper,
            IPaymentService<TransactionInitializeResponse, TransactionVerifyResponse, object> payStack)
        {
            _orderService = orderService;
            _payStack = payStack;
            _mapper = mapper;
        }

        public async Task<OrderToReturnDto> Handle(VerifyPaymentCommand request, CancellationToken cancellationToken)
        {
            var order = await _orderService.VerifyPayment(request.Trxref, _payStack);

            var orderToReturn = _mapper.Map<OrderEntity, OrderToReturnDto>(order);

            return orderToReturn;
        }
    }
}
