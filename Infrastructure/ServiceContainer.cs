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
            
            return services;
        }
    }
}
