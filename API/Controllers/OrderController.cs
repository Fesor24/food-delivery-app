using System.Net;
using API.Extensions;
using API.Helpers;
using Application.Dtos;
using Application.Features.Order.Requests.Commands;
using Application.Features.Order.Requests.Queries;
using Application.Response;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [ApiController]
    [Authorize]
    public class OrderController : ControllerBase
    {
        private readonly IMediator _mediator;

        public OrderController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost(Routes.CreateOrder)]
        public async Task<ApiResponse> CreateOrder([FromBody] OrderDto orderDto)
        {
            var userEmail = User.GetUsersEmailFromClaimsPrincipal();

            string paystackUrl = await _mediator.Send(new CreateOrderCommand { Email = userEmail, Order = orderDto });

            return new ApiResponse
            {
                Result = paystackUrl
            };
        }

        [HttpGet(Routes.GetOrderByIdAndEmail)]
        public async Task<ApiResponse> GetOrderByIdAndEmail([FromQuery] string orderId)
        {
            var email = User.GetUsersEmailFromClaimsPrincipal();

            var orderToReturn = await _mediator.Send(new GetOrderByIdAndEmailRequest { Email = email, OrderId = orderId });

            if(orderToReturn is null)
            {
                Response.StatusCode = (int)HttpStatusCode.NotFound;

                return new ApiResponse
                {
                    ErrorMessage = "Order not found"
                };
            }

            return new ApiResponse
            {
                Result = orderToReturn
            };
        }

        [HttpGet(Routes.GetOrdersForUser)]
        public async Task<ApiResponse> GetOrdersForUser()
        {
            var email = User.GetUsersEmailFromClaimsPrincipal();

            var orders = await _mediator.Send(new GetOrdersForUserRequest { Email = email });

            return new ApiResponse
            {
                Result = orders
            };
        }

        [AllowAnonymous]
        [HttpGet(Routes.VerifyPayment)]
        public async Task<ApiResponse> VerifyPayment([FromQuery] string trxref)
        {
            var order = await _mediator.Send(new VerifyPaymentCommand { Trxref = trxref });

            return new ApiResponse
            {
                Result = order
            };
        }
    }
}
