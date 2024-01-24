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
    }
}



