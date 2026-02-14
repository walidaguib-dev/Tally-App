using Application.Commands.Users.Profiles;
using Application.Dtos.Users.Profiles;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Validators.Users.Profiles
{
    public class CreateUserProfileValidations : AbstractValidator<CreateUserProfileCommand>
    {
        public CreateUserProfileValidations()
        {
            RuleFor(x => x.Dto.FirstName)
                .NotEmpty().WithMessage("First name is required.")
                .MaximumLength(50).WithMessage("First name cannot exceed 50 characters.")
                .WithName("First Name");
            RuleFor(x => x.Dto.LastName)
                .NotEmpty().WithMessage("Last name is required.")
                .MaximumLength(50).WithMessage("Last name cannot exceed 50 characters.")
                .WithName("Last Name");
            RuleFor(x => x.Dto.Bio)
                .MaximumLength(500).WithMessage("Bio cannot exceed 500 characters.")
                .When(x => x.Dto.Bio != null && x.Dto.Bio != string.Empty)
                .WithName("Bio");
            RuleFor(x => x.Dto.UserId)
                .NotEmpty().WithMessage("User ID is required.")
                .WithName("User Id");
            RuleFor(x => x.Dto.UploadId)
                .NotNull().GreaterThan(0).WithMessage("Upload ID is required.")
                .WithName("Upload Id");
        }
    }
}
