namespace EstoqFy.Core.Contracts.Repository
{
    public interface IBaseRepository<T> where T : class
    {
        Task CreateAsync(T entity);
        Task DeleteAsync(T entity);
        Task UpdateAsync(T entity);
        Task<T> GetByIdAsync(Guid id);
    }
}
