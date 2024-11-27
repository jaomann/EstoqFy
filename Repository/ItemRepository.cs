using EstoqFy.Core.Contracts.Repository;
using EstoqFy.Core.Entities;
using EstoqFy.Database;
using Microsoft.EntityFrameworkCore;
using System.Reflection.Metadata.Ecma335;

namespace EstoqFy.Repository
{
    public class ItemRepository : BaseRepository<Item>, IItemRepository
    {
        private readonly Context _context;
        public ItemRepository(Context context) : base(context)
        {
            _context = context;
        }

        public int Count(string EAN) => _context.Set<Item>()
            .FirstOrDefault(x => x.EAN == EAN && !x.IsDeleted).Count;

        public async Task<bool> ExistsAsync(string EAN, Guid user) => await _context.Set<Item>().AnyAsync(x => x.EAN == EAN && x.UserId == user && !x.IsDeleted);

        public async Task<Item> GetByEANAsync(string EAN, Guid user) => await _context.Set<Item>().FirstOrDefaultAsync(x => x.EAN == EAN && x.UserId == user && !x.IsDeleted);
    }
}
