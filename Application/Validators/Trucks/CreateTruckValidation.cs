using System;
using System.Collections.Generic;
using System.Text;
using Application.Commands.Trucks;
using Application.Dtos.Trucks;
using FluentValidation;

namespace Application.Validators.Trucks
{
    public class CreateTruckValidation : AbstractValidator<CreateTruckCommand>
    {
        public CreateTruckValidation()
        {
            RuleFor(x => x.Dto.PlateNumber)
                .NotEmpty()
                .WithMessage("Plate number is required.")
                .MinimumLength(7)
                .WithMessage("Plate number must be at least 7 characters.");

            RuleFor(x => x.Dto.Capacity)
                .GreaterThan(0)
                .WithMessage("Capacity must be greater than 0.");
        }
    }
}
