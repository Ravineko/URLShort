using Microsoft.EntityFrameworkCore;

namespace URLShort.Models
{
    public class ShortenerDbContext : DbContext
    {
        public ShortenerDbContext(DbContextOptions<ShortenerDbContext> options) 
            : base(options)
        {
        }
        public DbSet<User> Users { get; set; }
        public DbSet<LinkModel> Links { get; set; }
        public DbSet<AboutText> AboutTexts { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<AboutText>()
                .HasNoKey();
        }

    }
}



