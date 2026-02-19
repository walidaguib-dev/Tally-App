using Application.Helpers;
using Domain.Contracts;
using Domain.Helpers;
using Infrastructure.Repositories;
using MediatR;
namespace API.Services
{
    public static class SetupServices
    {
        public static IServiceCollection AddAPIServices(this IServiceCollection services)
        {
            // custom services 
            services.AddScoped<IUser, UsersRepository>();
            services.AddScoped<ITokens, TokensRepository>();
            services.AddScoped<IEmail, EmailsRepository>();
            services.AddScoped<ICaching , CachingRepository>();
            services.AddScoped<IUploads, UploadsRepository>();
            services.AddScoped<IUserProfile , UserProfilesRepository>();
            services.AddScoped<IShips, ShipsRepository>();
            services.AddScoped<IMerchandise, MerchandisesRepository>();
            services.AddScoped<IClients, ClientsRepository>();
            services.AddScoped<ITrucks, TrucksRepository>();
            // pipelines services
            services.AddTransient(typeof(IPipelineBehavior<,>), typeof(CacheInvalidationBehavior<,>));
            services.AddTransient(typeof(IPipelineBehavior<,>), typeof(ValidationBehaviour<,>));

            // Register API services here
            return services;
        }


    }
}
