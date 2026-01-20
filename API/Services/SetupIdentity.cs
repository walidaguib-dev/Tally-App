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
            services.AddIdentity<User, IdentityRole>()
                    .AddEntityFrameworkStores<ApplicationDbContext>()
                    .AddDefaultTokenProviders();
            // Register Identity services here
            return services;
        }
    }
}
