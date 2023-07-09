namespace Core.Entities
{
    public class Restaurant : BaseEntity
    {
        public string Name { get; set; }

        public string PictureUrl { get; set; }

        public string Address { get; set; }

        public float DeliveryFee { get; set; }

        public float Ratings { get; set; }

        public int Reviews { get; set; }
        
        public List<Products> Products { get; set; }
    }
}
