using Core.Entities.OrderAggregate;

namespace Core.Specifications
{
    public class OrderByIdAndEmailSpecification : BaseSpecification<Order>
    {
        public OrderByIdAndEmailSpecification(string email, string id): base(x => x.Id == id && x.CustomerEmail == email)
        {
            AddIncludes(x => x.OrderItems);
        }
    }
}
