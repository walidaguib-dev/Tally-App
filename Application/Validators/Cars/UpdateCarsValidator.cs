using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Application.Commands.Cars;
using Domain.Enums;
using FluentValidation;

namespace Application.Validators.Cars
{
    public class UpdateCarsValidator : AbstractValidator<UpdateCarsCommand>
    {
        private static readonly string[] AllowedStatuses =
        [
            .. Enum.GetNames<CarStatus>().Select(x => x.ToLower()),
        ];

        public UpdateCarsValidator()
        {
            RuleFor(x => x.Dto.Brand)
                .NotEmpty()
                .WithMessage("Brand is required.")
                .MaximumLength(100)
                .WithMessage("Brand cannot exceed 100 characters.");

            RuleFor(x => x.Dto.Type)
                .NotEmpty()
                .WithMessage("Type is required.")
                .MaximumLength(100)
                .WithMessage("Type cannot exceed 100 characters.");

            RuleFor(x => x.Dto.VinNumber)
                .NotEmpty()
                .WithMessage("VIN number is required.")
                .Length(6)
                .WithMessage("VIN number must be exactly 6 characters.")
                .Matches("^[0-9]+$")
                .WithMessage("VIN number must contain digits only.");

            RuleFor(x => x.Dto.carStatus)
                .NotEmpty()
                .WithMessage("Car status is required.")
                .Must(x => AllowedStatuses.Contains(x.ToLower()))
                .WithMessage($"Invalid status. Valid values: {string.Join(", ", AllowedStatuses)}");
            RuleFor(x => x.Dto.ShipId)
                .GreaterThan(0)
                .WithMessage("Ship Id must be greater than 0.");
        }
    }
}
