using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Core.Entities;
using Core.Entities.OrderAggregate;

namespace Core.Specifications
{
    public class OrderByIdSpecification : BaseSpecification<Order>
    {
        public OrderByIdSpecification(string orderId) : base(x => x.Id == orderId)
        {
            AddIncludes(x => x.OrderItems);
        }
    }
}
