using Application.Dtos.Mail;
using Application.Dtos.Tokens;
using Application.Dtos.Users;
using Application.Validators.Tokens;
using Application.Validators.Users;
using Application.Validators.Emails;
using FluentValidation;
using EcommerceApi.validations.Users;
using Application.Dtos.Users.Profiles;
using Application.Validators.Users.Profiles;
using Application.Dtos.Ships;
using Application.Validators.Ships;

namespace API.Services
{
    public static class Validations
    {
        public static void AddValidations(this IServiceCollection services)
        {
            // register user auth validations
            services.AddKeyedScoped<IValidator<RegisterUserDto>, RegisterUserValidations>("Register");
            services.AddKeyedScoped<IValidator<RefreshTokenRequest>, RefreshTokenRequestValidation>("GenerateToken");
            services.AddKeyedScoped<IValidator<SignInDto>, SignInValidations>("SignIn");
            services.AddKeyedScoped<IValidator<PasswordResetDto>, PasswordResetValidations>("PasswordReset");
            services.AddKeyedScoped<IValidator<SendEmailDto>, SendEmailValidation>("EmailValidator");
            services.AddKeyedScoped<IValidator<ForgetPasswordDto>, ForgetPasswordResetValidator>("ForgetPasswordReset");

            //register user profile validations
            services.AddKeyedScoped<IValidator<CreateUserProfileDto>, CreateUserProfileValidations>("CreateUserProfile");
            services.AddKeyedScoped<IValidator<UpdateUserProfileDto>, UpdateUserProfileValidations>("UpdateUserProfile");
            // register ships validations
             services.AddKeyedScoped<IValidator<CreateShipDto>, CreateShipValidation>("createShip");
             services.AddKeyedScoped<IValidator<UpdateShipDto>, UpdateShipValidation>("updateShip");
        }
    }
}
