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
using Application.Dtos.Merchandises;
using Application.Validators.Merchandises;
using Application.Commands.Users;
using Application.Commands.Tokens;
using Application.Commands.Emails;
using Application.Commands.Users.Profiles;
using Application.Commands.ships;
using Application.Commands.Merchandises;
using Application.Validators.Clients;
using Application.Validators.Trucks;
using Application.Validators.TallySheets;
using Application.Validators.Pagination;

namespace API.Services
{
    public static class Validations
    {
        public static void AddValidations(this IServiceCollection services)
        {
            // register user auth validations
            services.AddValidatorsFromAssemblyContaining<RegisterUserValidations>();
            // services.AddValidatorsFromAssembly(typeof(RefreshTokenRequestValidation).Assembly);
            // services.AddValidatorsFromAssembly(typeof(SignInValidations).Assembly);
            // services.AddValidatorsFromAssembly(typeof(PasswordResetValidations).Assembly);

            // services.AddValidatorsFromAssembly(typeof(SendEmailValidation).Assembly);
            // services.AddValidatorsFromAssembly(typeof(ForgetPasswordResetValidator).Assembly);

            // //register user profile validations
            // services.AddValidatorsFromAssembly(typeof(CreateUserProfileValidations).Assembly);
            // services.AddValidatorsFromAssembly(typeof(UpdateUserProfileValidations).Assembly);
            // // register ships validations
            // services.AddValidatorsFromAssembly(typeof(CreateShipValidation).Assembly);
            // services.AddValidatorsFromAssembly(typeof(UpdateShipValidation).Assembly);

            // // register Merchandise validations
            // services.AddValidatorsFromAssembly(typeof(CreateMerchandiseValidation).Assembly);
            // services.AddValidatorsFromAssembly(typeof(UpdateMerchandiseValidation).Assembly);

            // // register clients validations
            // services.AddValidatorsFromAssembly(typeof(CreateClientValidation).Assembly);
            // services.AddValidatorsFromAssembly(typeof(UpdateClientValidation).Assembly);
            // services.AddValidatorsFromAssembly(typeof(ClientsFilteringValidation).Assembly);

            // //register trucks validations
            // services.AddValidatorsFromAssembly(typeof(CreateTruckValidation).Assembly);
            // services.AddValidatorsFromAssembly(typeof(UpdateTruckValidation).Assembly);

            // // register tally sheets validations 
            // services.AddValidatorsFromAssembly(typeof(CreateTallySheetDtoValidator).Assembly);
            // services.AddValidatorsFromAssembly(typeof(UpdatetallySheetValidation).Assembly);

            // // register pagination validationscls

            // services.AddValidatorsFromAssembly(typeof(PaginationParamsValidator).Assembly);
        }
    }
}
