using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using Application.Commands.Users;
using Application.Dtos.Users;
using FluentValidation;

namespace Application.Validators.Users
{
    public class PasswordResetValidations : AbstractValidator<PasswordResetCommand>
    {
        public PasswordResetValidations()
        {
            RuleFor(x => x.Dto.userId)
                .NotEmpty()
                .NotNull()
                .WithMessage("user id is required!")
                .WithName("User Id");
            RuleFor(x => x.Dto.current_password)
                .NotEmpty()
                .NotNull()
                .MinimumLength(12)
                .MaximumLength(20)
                .WithMessage("current password is required!")
                .WithName("Current password");
            RuleFor(x => x.Dto.new_password)
                .NotEmpty()
                .NotNull()
                .MinimumLength(12)
                .MaximumLength(20)
                .WithMessage("new password is required!")
                .WithName("New Password")
                ;
        }
    }
}