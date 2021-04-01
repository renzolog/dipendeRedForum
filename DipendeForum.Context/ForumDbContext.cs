using DipendeForum.Context.Entities;
using Microsoft.EntityFrameworkCore;

namespace DipendeForum.Context
{
    public class ForumDbContext : DbContext
    {
        public ForumDbContext() {}

        public ForumDbContext(DbContextOptions<ForumDbContext> options) : base(options)
        {
        }

        public virtual DbSet<User> User { get; set; }
        public virtual DbSet<Message> Message { get; set; }
        public virtual DbSet<Post> Post { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>();
            modelBuilder.Entity<Post>();
            modelBuilder.Entity<Message>();
        }
    }
}