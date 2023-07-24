using Core.Entities.OrderAggregate;

namespace Core.Specifications
{
    public class OrdersForUserSpecification : BaseSpecification<Order>
    {
        public OrdersForUserSpecification(string email) : base(x => x.CustomerEmail == email)
        {
            AddIncludes(x => x.OrderItems);
            AddOrderByDescExp(x => x.DateCreated);
        }
    }
}
