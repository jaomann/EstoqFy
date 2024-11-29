using EstoqFy.Core.Entities;

namespace EstoqFy.Core.Contracts.Services
{
    public interface IUserService : IBaseService<User>
    {
        Task<bool> ExistsAsync(string email, string cnpj);
        Task<User> GetByEmailAsync(string email);
    }
}
