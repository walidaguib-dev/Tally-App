using Domain.Contracts;
using Infrastructure.Repositories;
namespace API.Services
{
    public static class SetupServices
    {
        public static IServiceCollection AddAPIServices(this IServiceCollection services)
        {
            services.AddScoped<IUser, UsersRepository>();
            services.AddScoped<ITokens, TokensRepository>();
            // Register API services here
            return services;
        }


    }
}
