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

       
      

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            var roles = new List<IdentityRole>
            {
                new IdentityRole
                {
                    Id = "11111111-1111-1111-1111-111111111111",
                    Name = "admin",
                    NormalizedName = "ADMIN",
                    ConcurrencyStamp = "11111111-aaaa-bbbb-cccc-111111111111"

                },
                new IdentityRole
                {
                    Id = "22222222-2222-2222-2222-222222222222",
                    Name = "user",
                    NormalizedName = "USER",
                    ConcurrencyStamp = "11111111-aaaa-bbbb-cccc-111111111111"

                }
            };

            builder.Entity<IdentityRole>().HasData(roles);
        }
    }
}