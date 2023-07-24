using Core.Entities.OrderAggregate;

namespace Core.Interfaces
{
    public interface IOrderService
    {
        Task<Order> CreateOrderAsync(string customerEmail, string cartId, Address shippingAddress);

        Task<Order> GetOrderByIdAndEmailAsync(string customerEmail, string orderId);

        Task<IReadOnlyList<Order>> GetOrdersForUserAsync(string email);
    }
}
