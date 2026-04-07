using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Persistence.Configurations.User;

namespace Persistence.Contexts
{
    internal class UserDbContext : IdentityDbContext
    {
        public UserDbContext(DbContextOptions<UserDbContext> dbContextOptions) : base(dbContextOptions) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.ApplyConfiguration(new UserEmployeeConfiguration());
            modelBuilder.Entity<IdentityUser>().ToTable("Users", "user");
            modelBuilder.Entity<IdentityRole>().ToTable("Roles", "user");
            modelBuilder.Entity<IdentityUserRole<string>>().ToTable("UserRoles", "user");
            modelBuilder.Entity<IdentityUserClaim<string>>().ToTable("UserClaims", "user");
            modelBuilder.Entity<IdentityUserLogin<string>>().ToTable("UserLogins", "user");
            modelBuilder.Entity<IdentityRoleClaim<string>>().ToTable("RoleClaims", "user");
            modelBuilder.Entity<IdentityUserToken<string>>().ToTable("UserTokens", "user");
        }
    }
}
