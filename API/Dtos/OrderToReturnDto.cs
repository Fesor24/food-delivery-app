using Core.Entities.OrderAggregate;

namespace API.Dtos
{
    public class OrderToReturnDto
    {
        public string Id { get; set; }

        public string CustomerEmail { get; set; }

        public Address DeliveryAddress { get; set; }

        public float SubTotal { get; set; }

        public float DeliveryCharges { get; set; }

        public IReadOnlyList<OrderItemDto> OrderItems { get; set; }

        public string Status { get; set; }

        public float Total { get; set; }

        public string DateCreated { get; set; }
    }
}
