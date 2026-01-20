using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using Application.Dtos.Users;
using FluentValidation;

namespace Application.Validators.Users
{
    public class PasswordResetValidations : AbstractValidator<PasswordResetDto>
    {
        public PasswordResetValidations()
        {
            RuleFor(x => x.userId)
                .NotEmpty()
                .NotNull()
                .WithMessage("user id is required!");
            RuleFor(x => x.current_password)
                .NotEmpty()
                .NotNull()
                .MinimumLength(12)
                .MaximumLength(20)
                .WithMessage("user id is required!");
            RuleFor(x => x.new_password)
                .NotEmpty()
                .NotNull()
                .MinimumLength(12)
                .MaximumLength(20)
                .WithMessage("user id is required!");
        }
    }
}