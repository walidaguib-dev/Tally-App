using Application.Dtos.Users;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Validators.Users
{
    public class ForgetPasswordResetValidator : AbstractValidator<ForgetPasswordDto>
    {
        public ForgetPasswordResetValidator()
        {
            RuleFor(x => x.userId)
                .NotEmpty().WithMessage("User ID is required.");
            RuleFor(x => x.Token)
                .NotEmpty().WithMessage("Token is required.");
            RuleFor(x => x.new_password)
                .NotEmpty().WithMessage("New password is required.")
                .MinimumLength(12).WithMessage("New password must be at least 12 characters long.");
        }
    }
}
