using System.Text.Json;
using Core.Entities;
using Core.Interfaces;
using StackExchange.Redis;

namespace Infrastructure.Repository
{
    public class ShoppingCartRepository : IShoppingCartRepository
    {
        private readonly IDatabase _db;

        public ShoppingCartRepository(IConnectionMultiplexer redis)
        {
            _db = redis.GetDatabase();
        }

        public async Task<bool> DeleteShoppingCartAsync(string shoppingCartId)
        {
            return await _db.KeyDeleteAsync(shoppingCartId);
        }

        public async Task<ShoppingCart> GetShoppingCartAsync(string shoppingCartId)
        {
            var shoppingCart = await _db.StringGetAsync(shoppingCartId);

            return shoppingCart.IsNullOrEmpty ? null : JsonSerializer.Deserialize<ShoppingCart>(shoppingCart);
        }

        public async Task<ShoppingCart> UpdateShoppingCartAsync(ShoppingCart shoppingCart)
        {
            var created = await _db.StringSetAsync(shoppingCart.Id, JsonSerializer.Serialize(shoppingCart), TimeSpan.FromDays(20));

            if (!created)
            {
                return null;
            }

            return await GetShoppingCartAsync(shoppingCart.Id);
        }
    }
}
