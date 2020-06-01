using Microsoft.EntityFrameworkCore;
using ShoesStoreApi.Models;

namespace ShoesStoreApi.Data {
    public class ShoesStoreApiContext : DbContext {
        public ShoesStoreApiContext (DbContextOptions<ShoesStoreApiContext> options) : base (options) { }
        public DbSet<Product> Products { get; set; }
        public DbSet<Cart> Carts { get; set; }
        public DbSet<CartItem> CartItems { get; set; }

    }

}