namespace Application.Dtos
{
    public class OrderItemDto
    {
        public int Quantity { get; set; }

        public float Price { get; set; }

        public string ProductId { get; set; }

        public string ProductName { get; set; }

        public string PictureUrl { get; set; }
    }
}
