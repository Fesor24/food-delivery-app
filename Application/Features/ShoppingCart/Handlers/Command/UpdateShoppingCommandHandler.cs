using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Application.Dtos;
using Application.Features.ShoppingCart.Requests.Command;
using AutoMapper;
using CartEntity = Core.Entities.ShoppingCart;
using Core.Interfaces;
using MediatR;

namespace Application.Features.ShoppingCart.Handlers.Command
{
    public class UpdateShoppingCommandHandler : IRequestHandler<UpdateShoppingCartCommand, CartEntity>
    {
        private readonly IShoppingCartRepository _shoppingCart;

        private readonly IMapper _mapper;

        public UpdateShoppingCommandHandler(IShoppingCartRepository shoppingCart, IMapper mapper)
        {
            _shoppingCart = shoppingCart;
            _mapper = mapper;
        }

        public async Task<CartEntity> Handle(UpdateShoppingCartCommand request, CancellationToken cancellationToken)
        {
            var cart = _mapper.Map<ShoppingCartDto, CartEntity>(request.ShoppingCart);

            var cartEntity = await _shoppingCart.UpdateShoppingCartAsync(cart);

            return cartEntity is null ? new CartEntity(cart.Id) : await _shoppingCart.GetShoppingCartAsync(cartEntity.Id);
        }
    }
}
