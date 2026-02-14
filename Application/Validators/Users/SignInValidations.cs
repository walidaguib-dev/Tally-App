using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Application.Commands.Users;
using Application.Dtos.Users;
using FluentValidation;

namespace EcommerceApi.validations.Users
{
    public class SignInValidations : AbstractValidator<SignInCommand>
    {
        public SignInValidations()
        {
            RuleFor(u => u.Dto.username)
                           .NotNull().NotEmpty().Length(8, 20)
                           .WithMessage("username must have between 8 and 20 letters").WithName("Username");
            RuleFor(u => u.Dto.password)
                .NotNull().NotEmpty().MinimumLength(12)
                .WithMessage("password must have 12 letters")
                .WithName("Password");
        }
    }
}