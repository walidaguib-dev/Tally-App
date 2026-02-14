using Application.Commands.Users;
using Application.Dtos.Users;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Validators.Users
{
    public class ForgetPasswordResetValidator : AbstractValidator<ForgetPasswordCommand>
    {
        public ForgetPasswordResetValidator()
        {
            RuleFor(x => x.Dto.userId)
                .NotEmpty().WithMessage("User ID is required.")
                .WithName("User Id");
            RuleFor(x => x.Dto.Token)
                .NotEmpty().WithMessage("Token is required.")
                .WithName("Token");
            RuleFor(x => x.Dto.new_password)
                .NotEmpty().WithMessage("New password is required.")
                .MinimumLength(12).WithMessage("New password must be at least 12 characters long.")
                .WithName("New password");
        }
    }
}
