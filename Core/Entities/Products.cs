using System.ComponentModel.DataAnnotations.Schema;

namespace Core.Entities
{
    public class Products : BaseEntity
    {
        public string Name { get; set; }

        public float Price { get; set; }

        public string PictureUrl { get; set; }

        public string RestaurantId { get; set; }

        [ForeignKey(nameof(RestaurantId))]
        public Restaurant Restaurant { get; set; }
    }
}
