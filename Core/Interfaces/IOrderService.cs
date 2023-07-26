using Core.Entities.OrderAggregate;
using PayStack.Net;

namespace Core.Interfaces
{
    public interface IOrderService
    {
        Task<Order> CreateOrderAsync(string customerEmail, string cartId, Address shippingAddress);

        Task<Order> GetOrderByIdAndEmailAsync(string customerEmail, string orderId);

        Task<IReadOnlyList<Order>> GetOrdersForUserAsync(string email);

        Task<Order> VerifyPayment(string trxref, IPaymentService<TransactionInitializeResponse, TransactionVerifyResponse, object> payStack);
    }
}
