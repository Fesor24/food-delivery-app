using Application.Dtos;
using Application.Features.Order.Requests.Commands;
using AutoMapper;
using Core.Interfaces;
using Core.Models;
using MediatR;
using PayStack.Net;

namespace Application.Features.Order.Handlers.Commands
{
    public class CreateOrderCommandHandler : IRequestHandler<CreateOrderCommand, string>
    {
        private readonly IMapper _mapper;

        private readonly IOrderService _orderService;

        private readonly IPaymentService<TransactionInitializeResponse, TransactionVerifyResponse, object> _payStack;

        public CreateOrderCommandHandler(IOrderService orderService, IMapper mapper,
            IPaymentService<TransactionInitializeResponse, TransactionVerifyResponse, object> payStack)
        {
            _mapper = mapper;
            _orderService = orderService;
            _payStack = payStack;
        }

        public async Task<string> Handle(CreateOrderCommand request, CancellationToken cancellationToken)
        {
            var deliveryAddress = _mapper.Map<AddressDto, Core.Entities.OrderAggregate.Address>(request.Order.Address);

            var order = await _orderService.CreateOrderAsync(request.Email, request.Order.CartId, deliveryAddress);

            if(order is null)
            {
                return null;
            }

            var paymentResult = await _payStack.InitializeAsync(new Payment
            {
                Amount = order.GetTotal(),
                Email = request.Email,
                OrderId = order.Id,
                CallbackUrl = request.Order.CallbackUrl
            });

            string paymentUrl = "";

            if (paymentResult.Successful)
            {
                paymentUrl = paymentResult.Result.Data.AuthorizationUrl;
            }

            return paymentUrl;
        }
    }
}
