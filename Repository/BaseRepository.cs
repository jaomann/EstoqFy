using EstoqFy.Core.Contracts.Repository;
using EstoqFy.Core.Entities;
using EstoqFy.Database;
using Microsoft.EntityFrameworkCore;

namespace EstoqFy.Repository
{
    public class BaseRepository<T> : IBaseRepository<T> where T : EntityBase
    {
        private readonly Context _context;
        public BaseRepository(Context context)
        {
            _context = context;
        }

        public async Task CreateAsync(T entity)
        {
            _context.Set<T>().Add(entity);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(T entity)
        {
            entity.IsDeleted = true;
            _context.Set<T>().Update(entity);
            await _context.SaveChangesAsync();
        }

        public async Task<List<T>> GetAllAsync() => await _context.Set<T>().ToListAsync();

        public async Task UpdateAsync(T entity)
        {
            _context.Set<T>().Update(entity);
            await _context.SaveChangesAsync();
        }
    }
}
