using Domain.Contracts;
using Infrastructure.Repositories;
using Infrastructure.Services;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Text;

namespace Infrastructure
{
    public static class ServicesContainer
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services , WebApplicationBuilder builder)
        {
            //services.ConfigureCloudinary(builder);
            ////services.AddAuthenticationServices(builder);
            //services.ConfigureRedisServices(builder.Configuration);
            //services.ConfigureEmailService(builder);
            //services.ConfigureBackgroundJobs(builder);
            //services.AddDatabase(builder.Configuration);
            // Register infrastructure services here
            return services;
        }
    }
}
