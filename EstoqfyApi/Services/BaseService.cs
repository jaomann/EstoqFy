using EstoqFy.Core.Contracts.Repository;
using EstoqFy.Core.Contracts.Services;

namespace EstoqFy.Services
{
    public class BaseService<T> : IBaseService<T> where T : class
    {
        private readonly IBaseRepository<T> _repository;
        public BaseService(IBaseRepository<T> repository)
        {
            _repository = repository;
        }
        public Task CreateAsync(T entity) => _repository.CreateAsync(entity);
        public Task DeleteAsync(T entity) => _repository.DeleteAsync(entity);

        public Task<T> GetByIdAsync(Guid id) => _repository.GetByIdAsync(id);

        public Task UpdateAsync(T entity) => _repository.UpdateAsync(entity);

    }
}
