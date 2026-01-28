using Application.Dtos.Users.Profiles;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Validators.Users.Profiles
{
    public class UpdateUserProfileValidations : AbstractValidator<UpdateUserProfileDto>
    {
        public UpdateUserProfileValidations()
        {
            RuleFor(x => x.FirstName)
                .NotEmpty().WithMessage("First name is required.")
                .MaximumLength(50).WithMessage("First name cannot exceed 50 characters.");
            RuleFor(x => x.LastName)
                .NotEmpty().WithMessage("Last name is required.")
                .MaximumLength(50).WithMessage("Last name cannot exceed 50 characters.");
            RuleFor(x => x.Bio)
                .MaximumLength(500).WithMessage("Bio cannot exceed 500 characters.")
                .When(x => x.Bio != null && x.Bio != string.Empty);
            RuleFor(x => x.UploadId)
                .NotNull().GreaterThan(0).WithMessage("Upload ID is required.");
        }
    }

}
