using System.ComponentModel.DataAnnotations;

namespace Application.Dtos
{
    public class ShoppingCartItemDto
    {
        [Required]
        public string Id { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        public string PictureUrl { get; set; }

        [Range(1, int.MaxValue, ErrorMessage = "Price can not be less than 1")]
        public float Price { get; set; }

        [Range(1, int.MaxValue, ErrorMessage = "Quantity can not be less than 1")]
        public int Quantity { get; set; }

        [Required]
        public string RestaurantId { get; set; }
    }
}
