using Application.Dtos.Mail;
using Application.Dtos.Tokens;
using Application.Dtos.Users;
using Application.Validators.Tokens;
using Application.Validators.Users;
using Application.Validators.Emails;
using FluentValidation;
using EcommerceApi.validations.Users;

namespace API.Services
{
    public static class Validations
    {
        public static void AddValidations(this IServiceCollection services)
        {
            services.AddKeyedScoped<IValidator<RegisterUserDto>, RegisterUserValidations>("Register");
            services.AddKeyedScoped<IValidator<RefreshTokenRequest>, RefreshTokenRequestValidation>("GenerateToken");
            services.AddKeyedScoped<IValidator<SignInDto> , SignInValidations>("SignIn");
            services.AddKeyedScoped<IValidator<PasswordResetDto>, PasswordResetValidations>("PasswordReset");
            services.AddKeyedScoped<IValidator<SendEmailDto>, SendEmailValidation>("EmailValidator");
            services.AddKeyedScoped<IValidator<ForgetPasswordDto>, ForgetPasswordResetValidator>("ForgetPasswordReset");
        }
    }
}
