using Microsoft.EntityFrameworkCore;
using UrlShortenerApi.Entities;

namespace UrlShortenerApi.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        public DbSet<User> Users { get; set; }
        public DbSet<ShortLink> ShortLinks { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>().HasIndex(u => u.Email).IsUnique();

            modelBuilder.Entity<ShortLink>().HasIndex(s => s.Code).IsUnique();

            base.OnModelCreating(modelBuilder);
        }
    }
}
