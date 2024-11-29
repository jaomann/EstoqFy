using EstoqFy.Core.Entities;

namespace EstoqFy.Core.Contracts.Services
{
    public interface IBaseService<T> where T : class
    {
        Task CreateAsync(T entity);
        Task DeleteAsync(T entity);
        Task UpdateAsync(T entity);
        Task<T> GetByIdAsync(Guid id);
    }
}
