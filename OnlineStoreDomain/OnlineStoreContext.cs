using Microsoft.EntityFrameworkCore;

namespace OnlineStore.Domain
{
    public class OnlineStoreContext : DbContext
    {
        public DbSet<Product> Products { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Order> Orders { get; set; }
        public OnlineStoreContext(DbContextOptions options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>().HasData(
                new User
                {
                    Name = "Narges1",
                },
                 new User
                 {
                     Name = "Narges2",
                 },
                   new User
                   {
                       Name = "Narges3",
                   },
                     new User
                     {
                         Name = "Narges4",
                     }
            );
        }

    }
}
