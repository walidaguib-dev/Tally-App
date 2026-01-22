using Domain.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Data
{
    public class ApplicationDbContext : IdentityDbContext<User>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }
        public DbSet<RefreshToken> refreshTokens { get; set; }
        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            var roles = new List<IdentityRole>
            {
                new IdentityRole
                {
                    Id = "11111111-1111-1111-1111-111111111111",
                    Name = "Admin",
                    NormalizedName = "ADMIN",
                    ConcurrencyStamp = "11111111-aaaa-bbbb-cccc-111111111111"

                },
                new IdentityRole
                {
                    Id = "22222222-2222-2222-2222-222222222222",
                    Name = "User",
                    NormalizedName = "USER",
                    ConcurrencyStamp = "11111111-aaaa-bbbb-cccc-111111111111"

                }
            };

            builder.Entity<IdentityRole>().HasData(roles);

            // Refresh tokens entity

            builder.Entity<RefreshToken>()
                .HasIndex(x => x.userId)
                .IsUnique();
            // Composite index for fast lookups
            builder.Entity<RefreshToken>()
                .HasIndex(rt => new { rt.userId, rt.Token })
                .HasDatabaseName("IX_RefreshTokens_UserId_Token");
        }
    }
}