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
            services.AddScoped<IUploads, UploadsRepository>();
            services.AddScoped<IUserProfile , UserProfilesRepository>();
            services.AddScoped<IShips, ShipsRepository>();
            services.AddScoped<IMerchandise, MerchandisesRepository>();
            // Register API services here
            return services;
        }


    }
}
