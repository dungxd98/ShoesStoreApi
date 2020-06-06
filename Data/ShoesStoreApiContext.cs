using Microsoft.EntityFrameworkCore;
using ShoesStoreApi.Models;

namespace ShoesStoreApi.Data {
    public class ShoesStoreApiContext : DbContext {
        public ShoesStoreApiContext (DbContextOptions<ShoesStoreApiContext> options) : base (options) { }
        public DbSet<Product> Products { get; set; }
        public DbSet<CartItem> CartItems { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<OrderDetails> OrderDetails { get; set; }

    }

}