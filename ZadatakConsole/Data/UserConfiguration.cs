using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using ZadatakConsole.Models;

namespace ZadatakConsole.Data
{
    public class UserConfiguration : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.ToTable("Users");

            builder.HasKey(c => c.Id);

            SeedData(builder);
        }
        private static void SeedData(EntityTypeBuilder<User> builder)
        {
            builder.HasData(
                           new User { Id = 1, Name = "Zana", Email = "zana@gmail.com" },
                           new User { Id = 2, Name = "Adis", Email = "adis@gmail.com" },
                           new User { Id = 3, Name = "Marijana", Email = "marijana@gmail.com" },
                           new User { Id = 4, Name = "Igor", Email = "igor@gmail.com" });
        }
    }
}
