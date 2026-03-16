using Application.Dtos.Mail;
using Application.Dtos.Tokens;
using Application.Dtos.Users;
using Application.Validators.Emails;
using Application.Validators.Tokens;
using Application.Validators.Users;
using EcommerceApi.validations.Users;
using FluentValidation;
using Microsoft.Extensions.DependencyInjection;

namespace Application
{
    public static class ServicesContainer
    {
        public static IServiceCollection AddApplication(this IServiceCollection services)
        {
            services.AddMediatR(cfg =>
                cfg.RegisterServicesFromAssembly(typeof(ServicesContainer).Assembly)
            );
            services.AddValidatorsFromAssembly(typeof(ServicesContainer).Assembly);
            return services;
        }
    }
}
