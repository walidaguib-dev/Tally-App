using Domain.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System.Reflection.Emit;

namespace Infrastructure.Data
{
    public class ApplicationDbContext : IdentityDbContext<User>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }
        public DbSet<RefreshToken> refreshTokens { get; set; }
        public DbSet<EmailToken> emailTokens { get; set; }
        public DbSet<UserProfile> profiles { get; set; }
        public DbSet<Pause> Pauses { get; set; }
        public DbSet<Uploads> uploads { get; set; }
        public DbSet<TallySheet> TallySheets { get; set; }
        public DbSet<Merchandise> Merchandises { get; set; }
        public DbSet<Ship> Ships { get; set; }
        public DbSet<Observation> Observations { get; set; }
        public DbSet<TallySheetClient> TallySheetClients { get; set; }
        public DbSet<TallySheetTruck> TallySheetTrucks { get; set; }
        public DbSet<Truck> Trucks { get; set; }
        public DbSet<Client> Clients { get; set; }
        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.ApplyConfigurationsFromAssembly(typeof(ApplicationDbContext).Assembly);
            base.OnModelCreating(builder);

            var roles = new List<IdentityRole>
            {
                new IdentityRole
                {
                    Id = "11111111-1111-1111-1111-111111111111",
                    Name = "Chef",
                    NormalizedName = "CHEF",
                    ConcurrencyStamp = "11111111-aaaa-bbbb-cccc-111111111111"

                },
                new IdentityRole
                {
                    Id = "22222222-2222-2222-2222-222222222222",
                    Name = "TallyMan",
                    NormalizedName = "TALLYMAN",
                    ConcurrencyStamp = "11111111-aaaa-bbbb-cccc-111111111111"

                }
            };

            builder.Entity<IdentityRole>().HasData(roles);




        }
    }
}