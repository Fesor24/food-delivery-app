using API.Helpers;
using Application.Dtos;
using Application.Features.ShoppingCart.Requests.Command;
using Application.Features.ShoppingCart.Requests.Queries;
using Application.Response;
using Core.Entities;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [ApiController]
    public class ShoppingCartController : ControllerBase
    {
        private readonly IMediator _mediator;

        public ShoppingCartController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet(Routes.GetShoppingCart)]
        public async Task<ApiResponse> GetShoppingCart([FromQuery] string shoppingCartId)
        {
            var shopppingCart = await _mediator.Send(new GetShoppingCartRequest { ShoppingCartId = shoppingCartId});

            return new ApiResponse
            {
                Result = shopppingCart is null ? new ShoppingCart(shoppingCartId) : shopppingCart
            };
        }

        [HttpPost(Routes.UpdateShoppingCart)]
        public async Task<ApiResponse> UpdateShoppingCart([FromBody] ShoppingCartDto shoppingCart)
        {
            var cart =await _mediator.Send(new UpdateShoppingCartCommand { ShoppingCart = shoppingCart });

            return new ApiResponse
            {
                Result = cart
            };
        }

        [HttpDelete(Routes.DeleteShoppingCart)]
        public async Task DeleteShoppingCart([FromQuery] string shoppingCartId)
        {
            await _mediator.Send(new DeleteShoppingCartCommand { ShoppingCartId = shoppingCartId});
        }
    }
}
