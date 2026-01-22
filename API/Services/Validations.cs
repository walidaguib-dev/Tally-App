using Application.Dtos.Tokens;
using Application.Dtos.Users;
using Application.Validators.Tokens;
using Application.Validators.Users;
using EcommerceApi.validations.Users;
using FluentValidation;

namespace API.Services
{
    public static class Validations
    {
        public static void AddValidations(this IServiceCollection services)
        {
            services.AddKeyedScoped<IValidator<RegisterUserDto>, RegisterUserValidations>("Register");
            services.AddKeyedScoped<IValidator<RefreshTokenRequest>, RefreshTokenRequestValidation>("GenerateToken");
            services.AddKeyedScoped<IValidator<SignInDto> , SignInValidations>("SignIn");
            // Register validation services here

        }
    }
}
