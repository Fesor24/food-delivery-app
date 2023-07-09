using Core.Entities;

namespace Core.Specifications
{
    public class ProductByRestaurantIdSpecification : BaseSpecification<Products>
    {
        public ProductByRestaurantIdSpecification(string restaurantId) : base(x => x.RestaurantId == restaurantId)
        {

        }
    }
}
