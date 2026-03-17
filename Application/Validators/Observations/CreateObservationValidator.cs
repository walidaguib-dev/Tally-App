using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Application.Commands.Observations;
using Domain.Enums;
using FluentValidation;

namespace Application.Validators.Observations
{
    public class CreateObservationDtoValidator : AbstractValidator<CreateObservationCommand>
    {
        private static readonly string[] AllowedTypes = Enum.GetNames<ObservationType>()
            .Select(x => x.ToLower())
            .ToArray();

        public CreateObservationDtoValidator()
        {
            RuleFor(x => x.Dto.Type)
                .NotEmpty()
                .WithMessage("Type is required.")
                .Must(x => AllowedTypes.Contains(x.ToLower()))
                .WithMessage($"Invalid type. Valid values: {string.Join(", ", AllowedTypes)}");

            RuleFor(x => x.Dto.Description)
                .NotEmpty()
                .WithMessage("Description is required.")
                .MaximumLength(500)
                .WithMessage("Description cannot exceed 500 characters.");

            RuleFor(x => x.Dto.TallySheetId).GreaterThan(0).WithMessage("Tally sheet is required.");

            RuleFor(x => x.Dto.ClientId)
                .GreaterThan(0)
                .WithMessage("Client Id must be greater than 0.")
                .When(x => x.Dto.ClientId.HasValue);

            RuleFor(x => x.Dto.TruckId)
                .GreaterThan(0)
                .WithMessage("Truck Id must be greater than 0.")
                .When(x => x.Dto.TruckId.HasValue);

            // Cannot target both client and truck
            RuleFor(x => x)
                .Must(x => !(x.Dto.ClientId.HasValue && x.Dto.TruckId.HasValue))
                .WithMessage("Observation cannot target both a client and a truck simultaneously.");
        }
    }
}
