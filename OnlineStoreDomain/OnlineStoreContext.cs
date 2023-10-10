using Microsoft.EntityFrameworkCore;

namespace OnlineStore.Domain
{
    public class OnlineStoreContext : DbContext
    {
        public DbSet<Product> Products { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Order> Orders { get; set; }
        public OnlineStoreContext(DbContextOptions options) : base(options) { }
    }
}
