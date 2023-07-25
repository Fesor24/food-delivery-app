using Core.Entities;

namespace Core.Specifications
{
    public class RestaurantByIdSpecification : BaseSpecification<Restaurant>
    {
        public RestaurantByIdSpecification(string restaurantId) : base(x =>  x.Id == restaurantId)
        {

        }
    }
}
