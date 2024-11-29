using EstoqFy.Core.Entities;

namespace EstoqFy.Core.Contracts.Repository
{
    public interface IItemRepository : IBaseRepository<Item>
    {
        int Count(string EAN);
        Task<bool> ExistsAsync(string EAN, Guid user);
        Task<Item> GetByEANAsync(string EAN, Guid user);
        Task<List<Item>> GetAllAsync(Guid user);
    }
}
