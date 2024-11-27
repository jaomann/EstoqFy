using EstoqFy.Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EstoqFy.Database.Configurations
{
    public class ItemConfiguration : IEntityTypeConfiguration<Item>
    {
        public void Configure(EntityTypeBuilder<Item> builder)
        {
            builder.HasKey(x => x.EAN);
            builder.Property(x => x.Name).IsRequired();
            builder.Property(x => x.Count).IsRequired();
            builder.Property(x => x.LastAdd).IsRequired();
            builder.HasOne(x => x.User).WithMany(x => x.Items).HasForeignKey(x => x.UserId);
        }
    }
}
