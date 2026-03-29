using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Application.Commands.Containers;
using Domain.Enums;
using FluentValidation;

namespace Application.Validators.Containers
{
    public class CreateContainerDtoValidator : AbstractValidator<CreateContainerCommand>
    {
        private static readonly string[] AllowedSizes =
        [
            .. Enum.GetNames<ContainerSize>().Select(e => e.ToLower()),
        ];
        private static readonly string[] AllowedTypes =
        [
            .. Enum.GetNames<ContainerType>().Select(e => e.ToLower()),
        ];
        private static readonly string[] AllowedStatuses =
        [
            .. Enum.GetNames<ContainerStatus>().Select(e => e.ToLower()),
        ];

        public CreateContainerDtoValidator()
        {
            // ISO 6346 container number format: 4 letters + 6 digits + 1 check digit
            // Example: MSCU1234567, CMAU9876543
            RuleFor(x => x.Dto.ContainerNumber)
                .NotEmpty()
                .WithMessage("Container number is required.")
                .Length(11)
                .WithMessage("Container number must be exactly 11 characters.")
                .Matches(@"^[A-Z]{3}[UJZ]\d{6}\d$")
                .WithMessage(
                    "Invalid container number format. Must follow ISO 6346: 3 owner letters + equipment category (U/J/Z) + 6 digits + 1 check digit. Example: MSCU1234567"
                );

            RuleFor(x => x.Dto.ContainerSize)
                .NotEmpty()
                .WithMessage("Container size is required.")
                .Must(x => AllowedSizes.Contains(x))
                .WithMessage(
                    $"Invalid size. Valid values: {string.Join(", ", AllowedSizes)} (feet)."
                );

            RuleFor(x => x.Dto.ContainerType)
                .NotEmpty()
                .WithMessage("Container type is required.")
                .Must(x => AllowedTypes.Contains(x.ToLower()))
                .WithMessage($"Invalid type. Valid values: {string.Join(", ", AllowedTypes)}.");

            RuleFor(x => x.Dto.ContainerStatus)
                .NotEmpty()
                .WithMessage("Container status is required.")
                .Must(x => AllowedStatuses.Contains(x.ToLower()))
                .WithMessage(
                    $"Invalid status. Valid values: {string.Join(", ", AllowedStatuses)}."
                );

            RuleFor(x => x.Dto.SealNumber)
                .MaximumLength(15)
                .WithMessage("Seal number cannot exceed 15 characters.")
                .When(x => x.Dto.SealNumber is not null);

            RuleFor(x => x.Dto.TallySheetId)
                .GreaterThan(0)
                .WithMessage("Tally sheet Id must be greater than 0.");

            RuleFor(x => x.Dto.userId).NotEmpty().WithMessage("user Id can not be empty.");

            RuleFor(x => x.Dto.ClientId).NotNull().WithMessage("user Id can not be null.");
        }
    }
}
