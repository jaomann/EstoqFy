using EstoqFy.Core.Entities;

namespace EstoqFy.Core.Contracts.Repository
{
    public interface IUserRepository : IBaseRepository<User>
    {
        Task<bool> ExistsAsync(string email, string cnpj);
        Task<User> GetByIdAsync(Guid Id);
        Task<User> GetByEmailAsync(string email);
    }
}
