using FluentValidation;
using Microsoft.Extensions.DependencyInjection;

namespace Application
{
    public static class DI
    {
        public static IServiceCollection AddApplication(this IServiceCollection services)
        {
            services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(typeof(DI).Assembly));
            services.AddValidatorsFromAssembly(typeof(DI).Assembly);
            return services;
        }
    }
}
