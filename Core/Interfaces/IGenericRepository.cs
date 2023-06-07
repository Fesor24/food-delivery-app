using Core.Entities;
using Core.Specifications;

namespace Core.Interfaces
{
    public interface IGenericRepository<T> where T: BaseEntity
    {
        Task<T> GetAsync(string id);

        Task<IReadOnlyList<T>> GetAllAsync();

        Task<T> GetWithSpecAsync(ISpecification<T> spec);

        Task<IReadOnlyList<T>> GetAllWIthSpecAsync(ISpecification<T> spec);

        Task AddListAsync(List<T> entities);
    }
}
