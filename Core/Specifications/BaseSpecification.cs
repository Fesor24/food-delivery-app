using System.Linq.Expressions;

namespace Core.Specifications
{
    public class BaseSpecification<T> : ISpecification<T>
    {

        public BaseSpecification()
        {

        }
        public BaseSpecification(Expression<Func<T, bool>> criteria)
        {
            Criteria = criteria;
        }

        public Expression<Func<T, bool>> Criteria { get; }

        public List<Expression<Func<T, object>>> Includes { get; } = new();

        public Expression<Func<T, object>> OrderByExp { get; private set; }

        public Expression<Func<T, object>> OrderByDescExp { get; private set; }

        protected void AddIncludes(Expression<Func<T, object>> includes)
        {
            Includes.Add(includes);
        }

        protected void AddOrderByExp(Expression<Func<T, object>> orderByExp)
        {
            OrderByExp = orderByExp;
        }

        protected void AddOrderByDescExp(Expression<Func<T, object>> orderByDescExp)
        {
            OrderByDescExp = orderByDescExp;
        }
    }
}
