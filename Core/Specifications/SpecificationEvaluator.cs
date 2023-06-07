using Core.Entities;
using Microsoft.EntityFrameworkCore;

namespace Core.Specifications
{
    public class SpecificationEvaluator<TEntity> where TEntity: BaseEntity
    {
        public static IQueryable<TEntity> GetQuery(IQueryable<TEntity> query, ISpecification<TEntity> spec)
        {
            if(spec.Criteria is not null)
            {
                query = query.Where(spec.Criteria);
            }

            query = spec.Includes.Aggregate(query, (current, include) => current.Include(include));


            return query;
        }
    }
}
