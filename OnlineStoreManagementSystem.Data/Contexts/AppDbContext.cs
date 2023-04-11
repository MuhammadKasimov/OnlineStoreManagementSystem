using Microsoft.EntityFrameworkCore;
using OnlineStoreManagementSystem.Domain.Entitties.Attachments;
using OnlineStoreManagementSystem.Domain.Entitties.Carts;
using OnlineStoreManagementSystem.Domain.Entitties.Products;
using OnlineStoreManagementSystem.Domain.Entitties.Users;

namespace OnlineStoreManagementSystem.Contexts
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {

        }

        public DbSet<User> Users { get; set; }
        public DbSet<Attachment> Attachments { get; set; }
        public DbSet<UserProduct> UserProducts { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<Cart> Carts { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>()
                .HasIndex(u => u.Username)
                .IsUnique(true);

            modelBuilder.Entity<User>()
                .HasOne(u => u.Cart)
                .WithOne()
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
