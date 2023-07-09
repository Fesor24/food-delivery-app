namespace API.Dtos
{
    public class ProductDto
    {
        public string Id { get; set; }
        public string Name { get; set; }

        public string Description { get; set; }

        public float Price { get; set; }

        public string PictureUrl { get; set; }

        public string RestaurantId { get; set; }
    }
}
