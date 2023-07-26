using System;
using Core.Entities;
using Core.Entities.OrderAggregate;
using Core.Interfaces;
using Core.Specifications;
using PayStack.Net;

namespace Infrastructure.Services
{
    public class OrderService : IOrderService
    {
        private readonly IUnitOfWork unitOfWork;
        private readonly IShoppingCartRepository shoppingCartRepo;
        

        public OrderService(IUnitOfWork unitOfWork, 
            IShoppingCartRepository shoppingCartRepo)
        {
            this.shoppingCartRepo = shoppingCartRepo;
            this.unitOfWork = unitOfWork;
        }

        public async Task<Order> CreateOrderAsync(string customerEmail, string cartId, Address shippingAddress)
        {
            var cart = await shoppingCartRepo.GetShoppingCartAsync(cartId);

            if(cart is null)
            {
                throw new Exception("Cart not found");
            }
            
            List<OrderItem> orderItems = new List<OrderItem>();

            HashSet<string> restaurantIds = new HashSet<string>();

            foreach(var item in cart.Items)
            {
                var product = await unitOfWork.Repository<Products>().GetAsync(item.Id);

                if(product is null)
                {
                    continue;
                }

                restaurantIds.Add(product.RestaurantId);

                ProductItemOrdered productItemOrdered = new ProductItemOrdered
                {
                    PictureUrl = product.PictureUrl,
                    ProductName = product.Name,
                    ProductItemId = item.Id
                };

                OrderItem orderItem = new OrderItem
                {
                    Price = product.Price,
                    Quantity = item.Quantity,
                    Id = Guid.NewGuid().ToString(),
                    ItemOrdered = productItemOrdered
                };

                orderItems.Add(orderItem);
            }

            var subtotal = orderItems.Sum(x => x.Quantity * x.Price);

            float deliveryCharges = 0;

            foreach(var id in restaurantIds)
            {
                var restaurant = await unitOfWork.Repository<Restaurant>().GetAsync(id);

                if(restaurant is not null)
                {
                    deliveryCharges += restaurant.DeliveryFee;
                }
            }

            Order order = new Order
            {
                OrderItems = orderItems,
                CustomerEmail = customerEmail,
                DeliveryAddress = shippingAddress,
                Id = Guid.NewGuid().ToString(),
                Status = OrderStatus.Pending,
                PaymentStatus = PaymentStatus.Pending,
                SubTotal = subtotal,
                DeliveryCharges = deliveryCharges
            };

            await unitOfWork.Repository<Order>().AddAsync(order);

            var result = await unitOfWork.CompleteAsync();

            if (result <= 0) return null;

            await shoppingCartRepo.DeleteShoppingCartAsync(cartId);

            return order;
        }

        public async Task<Order> GetOrderByIdAndEmailAsync(string customerEmail, string orderId)
        {
            var orderSpec = new OrderByIdAndEmailSpecification(customerEmail, orderId);

            var order = await unitOfWork.Repository<Order>().GetWithSpecAsync(orderSpec);

            return order;
        }

        public async Task<IReadOnlyList<Order>> GetOrdersForUserAsync(string email)
        {
            var orderSpec = new OrdersForUserSpecification(email);

            var orders = await unitOfWork.Repository<Order>().GetAllWIthSpecAsync(orderSpec);

            return orders;
        }

        public async Task<Order> VerifyPayment(string trxref, IPaymentService<TransactionInitializeResponse, TransactionVerifyResponse, object> payStack)
        {
            var spec = new PaymentTransactionByReferenceSpecification(trxref);

            var paymentTransaction = await unitOfWork.Repository<PaymentTransaction>().GetWithSpecAsync(spec);

            var orderSpec = new OrderByIdSpecification(paymentTransaction.OrderId);

            var order = await unitOfWork.Repository<Order>().GetWithSpecAsync(orderSpec);

            if(order.PaymentStatus is not PaymentStatus.Pending)
            {
                return order;
            }

            var paymentResult = await payStack.VerifyAsync(trxref);

            if (paymentResult.Successful && paymentResult.Result.Status)
            {
                paymentTransaction.Verified = true;

                order.Status = OrderStatus.Shipped;

                order.PaymentStatus = PaymentStatus.Successful;              
            }
            else
            {
                order.PaymentStatus = PaymentStatus.Failed;
            }

            unitOfWork.Repository<PaymentTransaction>().Update(paymentTransaction);

            unitOfWork.Repository<Order>().Update(order);

            await unitOfWork.CompleteAsync();

            return order;
        }
    }
}
