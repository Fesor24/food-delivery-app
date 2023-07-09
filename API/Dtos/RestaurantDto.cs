namespace API.Dtos
{
    public class RestaurantDto
    {
        public string Id { get; set; }

        public string Name { get; set; }

        public string PictureUrl { get; set; }

        public string Address { get; set; }

        public float Ratings { get; set; }

        public int Reviews { get; set; }

        public float DeliveryFee { get; set; }
    }
}
