using Application.Dtos.Users;
using Application.Validators.Users;
using FluentValidation;

namespace API.Services
{
    public static class Validations
    {
        public static void AddValidations(this IServiceCollection services)
        {
            services.AddKeyedScoped<IValidator<RegisterUserDto>, RegisterUserValidations>("Register");
            // Register validation services here
            
        }
    }
}
