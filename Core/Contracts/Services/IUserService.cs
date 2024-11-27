using EstoqFy.Core.Entities;

namespace EstoqFy.Core.Contracts.Services
{
    public interface IUserService : IBaseService<User>
    {
        Task<bool> ExistsAsync(string email, string cnpj);
        Task<User> GetByIdAsync(Guid Id);
        Task<User> GetByEmailAsync(string email);
        void SetCookieId(Guid id, HttpResponse response);
    }
}
