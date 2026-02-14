using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Application.Commands.Users;
using Application.Dtos.Users;
using Domain.Enums;
using FluentValidation;

namespace Application.Validators.Users
{
    public class RegisterUserValidations : AbstractValidator<RegisterUserCommand>
    {
        public RegisterUserValidations()
        {
            RuleFor(u => u.Dto.email)
                            .EmailAddress().NotEmpty().NotNull().WithMessage("please write a valid email").WithName("Email");
            RuleFor(u => u.Dto.username)
                .NotNull().NotEmpty().Length(8, 20)
                .WithMessage("username must have between 8 and 20 letters").WithName("Username");
            RuleFor(u => u.Dto.password)
                .NotNull().NotEmpty().MinimumLength(12)
                .WithMessage("password must have 12 letters").WithName("Password");
            RuleForEach(x => x.Dto.Role)
                .NotEmpty()
                .NotNull()
                .WithMessage("Invalid role specified.").WithName("Role");
        }
    }
}