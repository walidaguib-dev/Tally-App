using Domain.Contracts;
using Domain.Helpers;
using Infrastructure.Repositories;
namespace API.Services
{
    public static class SetupServices
    {
        public static IServiceCollection AddAPIServices(this IServiceCollection services)
        {
            services.AddScoped<IUser, UsersRepository>();
            services.AddScoped<ITokens, TokensRepository>();
            services.AddScoped<IEmail, EmailsRepository>();
            services.AddScoped<ICaching , CachingRepository>();
            // Register API services here
            return services;
        }


    }
}
