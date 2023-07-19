using API.Dtos;
using API.Helpers;
using API.Response;
using AutoMapper;
using Core.Entities;
using Core.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [ApiController]
    public class ShoppingCartController : ControllerBase
    {
        private readonly IShoppingCartRepository _shoppingCartRepo;

        private readonly IMapper _mapper;

        public ShoppingCartController(IShoppingCartRepository shoppingCartRepo, IMapper mapper)
        {
            _shoppingCartRepo= shoppingCartRepo;
            _mapper = mapper;
        }

        [HttpGet(Routes.GetShoppingCart)]
        public async Task<ApiResponse> GetShoppingCart([FromQuery] string shoppingCartId)
        {
            var shopppingCart = await _shoppingCartRepo.GetShoppingCartAsync(shoppingCartId);

            return new ApiResponse
            {
                Result = shopppingCart is null ? new ShoppingCart(shoppingCartId) : shopppingCart
            };
        }

        [HttpPost(Routes.UpdateShoppingCart)]
        public async Task<ApiResponse> UpdateShoppingCart([FromBody] ShoppingCartDto shoppingCart)
        {
            var cartEntity = _mapper.Map<ShoppingCartDto, ShoppingCart>(shoppingCart);

            var cart = await _shoppingCartRepo.UpdateShoppingCartAsync(cartEntity);

            return new ApiResponse
            {
                Result = cart is null ? new ShoppingCart(shoppingCart.Id) : cart
            };
        }

        [HttpDelete(Routes.DeleteShoppingCart)]
        public async Task DeleteShoppingCart([FromQuery] string shoppingCartId)
        {
            await _shoppingCartRepo.DeleteShoppingCartAsync(shoppingCartId);
        }
    }
}
