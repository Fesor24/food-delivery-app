using Microsoft.EntityFrameworkCore;

namespace Core.Entities.OrderAggregate
{
    public class Order : BaseEntity
    {
        public string CustomerEmail { get; set; }

        public Address DeliveryAddress { get; set; }

        public float SubTotal { get; set; }

        public float DeliveryCharges { get; set; }

        public IReadOnlyList<OrderItem> OrderItems { get; set; }

        public OrderStatus Status { get; set; }

        public PaymentStatus PaymentStatus { get; set; }

        public float GetTotal()
        {
            return SubTotal + DeliveryCharges;
        }
    }
}
