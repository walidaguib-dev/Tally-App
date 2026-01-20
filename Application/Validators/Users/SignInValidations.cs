using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Application.Dtos.Users;
using FluentValidation;

namespace EcommerceApi.validations.Users
{
    public class SignInValidations : AbstractValidator<SignInDto>
    {
        public SignInValidations()
        {
            RuleFor(u => u.username)
                           .NotNull().NotEmpty().Length(8, 20)
                           .WithMessage("username must have between 8 and 20 letters");
            RuleFor(u => u.password)
                .NotNull().NotEmpty().MinimumLength(12)
                .WithMessage("password must have 12 letters");
        }
    }
}