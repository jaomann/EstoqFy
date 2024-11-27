using EstoqFy.Core.Entities;

namespace EstoqFy.Core.Contracts.Services
{
    public interface IItemService : IBaseService<Item>
    {
        int Count(string EAN);
    }
}
