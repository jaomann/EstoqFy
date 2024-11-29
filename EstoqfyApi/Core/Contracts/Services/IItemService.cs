using EstoqFy.Core.Entities;

namespace EstoqFy.Core.Contracts.Services
{
    public interface IItemService : IBaseService<Item>
    {
        int Count(string EAN);
        Task<bool> ExistsAsync(string EAN, Guid user);
        Task<Item> GetByEANAsync(string EAN, Guid user);
        Task<List<Item>> GetAllAsync(Guid user);
    }
}
