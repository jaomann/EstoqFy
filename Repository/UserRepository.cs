using EstoqFy.Core.Contracts.Repository;
using EstoqFy.Core.Entities;
using EstoqFy.Database;
using Microsoft.EntityFrameworkCore;

namespace EstoqFy.Repository
{
    public class UserRepository : BaseRepository<User>, IUserRepository
    {
        private readonly Context _context;
        public UserRepository(Context context) : base(context)
        {
            _context = context;
        }

        public async Task<bool> ExistsAsync(string email, string cnpj) => await _context.Set<User>().AnyAsync(x=> x.Email == email && x.Cnpj == cnpj && !x.IsDeleted);

        public async Task<User> GetByIdAsync(Guid Id) => await _context.Set<User>().FirstOrDefaultAsync(x => x.Id == Id && !x.IsDeleted);
    }
}
