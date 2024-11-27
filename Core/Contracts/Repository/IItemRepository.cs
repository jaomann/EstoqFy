using EstoqFy.Core.Entities;

namespace EstoqFy.Core.Contracts.Repository
{
    public interface IItemRepository : IBaseRepository<Item>
    {
        int Count(string EAN);
        Task<bool> ExistsAsync(string EAN);
    }
}
