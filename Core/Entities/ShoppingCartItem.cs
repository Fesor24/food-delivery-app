namespace Core.Entities
{
    public class ShoppingCartItem
    {
        public string Id { get; set; }

        public string Name { get; set; }

        public string PictureUrl { get; set; }

        public float Price { get; set; }

        public int Quantity { get; set; }

        public string RestaurantId { get; set; }
    }
}
