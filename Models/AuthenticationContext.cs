using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace ShoesStoreApi.Models {
    public class AuthenticationContext : IdentityDbContext {
        public AuthenticationContext (DbContextOptions options) : base (options) {

        }
        public DbSet<ApplicationUser> ApplicationUsers { get; set; }
    }
}