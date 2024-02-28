using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using MyStore.Database.Configurations;
using MyStore.Database.Models.Category;
using MyStore.Database.Models.Product;

namespace MyStore.Database
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        private static readonly string adminRoleId = "94179066-BFAE-4893-A906-7D959288F64E";
        private static readonly string adminUserId = "AF6A6896-6B30-43C6-ABBD-33FEBB129804";
        public ApplicationDbContext() : base()
        {

        }
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {

        }

        public DbSet<ProductModel> Products { get; set; }
        public DbSet<CategoryModel> Categories { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            SeedRoles(builder);
            SeedUsers(builder);

            builder.ApplyConfiguration(new ProductConfiguration());
            builder.ApplyConfiguration(new CategoryConfiguration());
        }

        private static void SeedRoles(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<IdentityRole>().HasData(
                new IdentityRole() { Id = adminRoleId, Name = "Admin", ConcurrencyStamp = "1", NormalizedName = "Admin" },
                new IdentityRole() { Name = "User", ConcurrencyStamp = "2", NormalizedName = "User" }
                );
        }

        private static void SeedUsers(ModelBuilder modelBuilder)
        {
            var hasher = new PasswordHasher<ApplicationUser>();
            var user = new ApplicationUser
            {
                Id = adminUserId,
                FirstName = "Admin",
                LastName = "Admin",
                Email = "admin@example.com",
                UserName = "admin@example.com",
                EmailConfirmed = true,
                SecurityStamp = Guid.NewGuid().ToString("D"),
                PasswordHash = hasher.HashPassword(null, "Abc123456")
            };
            modelBuilder.Entity<ApplicationUser>().HasData(user);
            modelBuilder.Entity<IdentityUserRole<string>>().HasData(
                new IdentityUserRole<string>
                {
                    RoleId = adminRoleId,
                    UserId = adminUserId
                });
        }
    }
}