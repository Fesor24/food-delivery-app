using System.Linq.Expressions;
using Core.Entities;

namespace Core.Specifications
{
    public class RestaurantSpecification : BaseSpecification<Restaurant>
    {
        public RestaurantSpecification(string sort, string location = null)
            : base(x => string.IsNullOrWhiteSpace(location) || x.Address.ToLower().Contains(location.ToLower()))
        {
            if (!string.IsNullOrWhiteSpace(sort))
            {
                switch (sort)
                {
                    case "priceDesc":
                        AddOrderByDescExp(x => x.DeliveryFee);
                        break;
                    case "priceAsc":
                        AddOrderByExp(x => x.DeliveryFee);
                        break;
                    default:
                        AddOrderByExp(x => x.Name);
                        break;
                }
            }     
        }
    }
}
