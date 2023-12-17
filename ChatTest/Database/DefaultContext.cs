using ChatTest.Models;
using Microsoft.EntityFrameworkCore;

namespace ChatTest.Database
{
    public class DefaultContext : DbContext
    {
        public DbSet<Message> Messages { get; set; }

        public DbSet<User> Users { get; set; }

        public DbSet<TokenEntity> Tokens { get; set; }

        public DbSet<Chat> Chats { get; set; }

        public DbSet<ChatUser> ChatUsers { get; set; }

        public DefaultContext(DbContextOptions options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<User>()
                .HasIndex(u => u.Email)
                .IsUnique();
            builder.Entity<User>()
                .HasMany(x => x.Chats)
                .WithMany(x => x.Users)
                .UsingEntity<ChatUser>();
        }
    }
}
