using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using StackExchange.Redis;
using System;
using System.Collections.Generic;
using System.Text;

namespace Infrastructure.Services
{
    public static class AddRedis
    {
        public static IServiceCollection ConfigureRedisServices(this IServiceCollection services, IConfiguration configuration)
        {
            var redisConfiguration = configuration.GetValue<string>("RedisConnectionString")!;
            services.AddSingleton<IConnectionMultiplexer>
           (
               x => ConnectionMultiplexer.Connect(redisConfiguration)
           );
            return services;
        }
    }
}
