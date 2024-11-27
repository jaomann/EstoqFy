using EstoqFy.Core.Entities;
using Microsoft.EntityFrameworkCore;

namespace EstoqFy.Database
{
    public class Context : DbContext
    {
        public DbSet<Item> Item { get; set; }
        public DbSet<User> User { get; set; }

        public Context(DbContextOptions options) : base(options)
        {}
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(Context).Assembly);
        }
    }
}
