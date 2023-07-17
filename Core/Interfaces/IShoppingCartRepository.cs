using Core.Entities;

namespace Core.Interfaces
{
    public interface IShoppingCartRepository
    {
        Task<ShoppingCart> GetShoppingCartAsync(string shoppingCartId);

        Task<ShoppingCart> UpdateShoppingCartAsync(ShoppingCart shoppingCart);

        Task<bool> DeleteShoppingCartAsync(string shoppingCartId);
    }
}
