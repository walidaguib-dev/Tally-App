using Domain.Entities;
using Infrastructure.Data;
using Microsoft.AspNetCore.Identity;
using System;

namespace API.Services
{
    public static class SetupIdentity
    {
        public static IServiceCollection AddIdentityServices(this IServiceCollection services)
        {
            services.AddIdentity<User, IdentityRole>(options =>
            {
                options.Password.RequireDigit = true;
                options.Password.RequireLowercase = true;
                options.Password.RequiredLength = 12;
                options.User.RequireUniqueEmail = true;

            })
                    .AddEntityFrameworkStores<ApplicationDbContext>()
                    .AddRoles<IdentityRole>()
                    .AddDefaultTokenProviders();
            // Register Identity services here
            return services;
        }
    }
}
