using System.Net;
using API.Dtos;
using API.Extensions;
using API.Helpers;
using API.Response;
using AutoMapper;
using Core.Entities.OrderAggregate;
using Core.Interfaces;
using Core.Models;
using Core.Specifications;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PayStack.Net;

namespace API.Controllers
{
    [ApiController]
    [Authorize]
    public class OrderController : ControllerBase
    {
        private readonly IOrderService orderService;

        private readonly IMapper mapper;

        private readonly IPaymentService<TransactionInitializeResponse, TransactionVerifyResponse, object> payStack;

        public OrderController(IOrderService orderService, IMapper mapper,
            IPaymentService<TransactionInitializeResponse, TransactionVerifyResponse, object> payStack)
        {
            this.orderService = orderService;
            this.mapper = mapper;
            this.payStack = payStack;
        }

        [HttpPost(Routes.CreateOrder)]
        public async Task<ApiResponse> CreateOrder([FromBody] OrderDto orderDto)
        {
            string paystackUrl = "";

            var userEmail = User.GetUsersEmailFromClaimsPrincipal();

            var deliveryAddress = mapper.Map<AddressDto, Core.Entities.OrderAggregate.Address>(orderDto.Address);

            var order = await orderService.CreateOrderAsync(userEmail, orderDto.CartId, deliveryAddress);

            if(order is null)
            {
                Response.StatusCode = (int)HttpStatusCode.BadRequest;

                return new ApiResponse
                {
                    ErrorMessage = "An error occurred while creating your order"
                };
            }

            var paymentResult = await payStack.InitializeAsync(new Payment
            {
                Amount = order.GetTotal(),
                Email = userEmail,
                OrderId = order.Id,
                CallbackUrl = orderDto.CallbackUrl
            });

            if (paymentResult.Successful)
            {
                paystackUrl = paymentResult.Result.Data.AuthorizationUrl;
            }

            return new ApiResponse
            {
                Result = paystackUrl
            };
        }

        [HttpGet(Routes.GetOrderByIdAndEmail)]
        public async Task<ApiResponse> GetOrderByIdAndEmail([FromQuery] string orderId)
        {
            var email = User.GetUsersEmailFromClaimsPrincipal();

            var order = await orderService.GetOrderByIdAndEmailAsync(email, orderId);

            if(order is null)
            {
                Response.StatusCode = (int)HttpStatusCode.NotFound;

                return new ApiResponse
                {
                    ErrorMessage = "Order not found"
                };
            }

            var orderToReturn = mapper.Map<Order, OrderToReturnDto>(order);

            return new ApiResponse
            {
                Result = orderToReturn
            };
        }

        [HttpGet(Routes.GetOrdersForUser)]
        public async Task<ApiResponse> GetOrdersForUser()
        {
            var email = User.GetUsersEmailFromClaimsPrincipal();

            var orders = await orderService.GetOrdersForUserAsync(email);

            var ordersToReturn = mapper.Map<IReadOnlyList<Order>, IReadOnlyList<OrderToReturnDto>>(orders);

            return new ApiResponse
            {
                Result = ordersToReturn
            };
        }

        [AllowAnonymous]
        [HttpGet(Routes.VerifyPayment)]
        public async Task<ApiResponse> VerifyPayment([FromQuery] string trxref)
        {
            var order = await orderService.VerifyPayment(trxref, payStack);

            var orderToReturn = mapper.Map<Order, OrderToReturnDto>(order);

            return new ApiResponse
            {
                Result = orderToReturn
            };
        }
    }
}
