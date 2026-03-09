using Application.Commands.ships;
using Application.Dtos.Ships;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Validators.Ships
{
    public class CreateShipValidation : AbstractValidator<CreateShipCommand>
    {
        public CreateShipValidation()
        {
            RuleFor(x => x.Dto.Name)
                .NotEmpty().WithMessage("Ship name is required.")
                .MaximumLength(100).WithMessage("Ship name cannot exceed 100 characters.")
                .WithName("Ship Name");
            RuleFor(x => x.Dto.ImoNumber)
                 .NotEmpty().WithMessage("IMO number is required.")
                 .Matches(@"^\d{7}$").WithMessage("IMO number must be exactly 7 digits.")
                 .WithName("Ship Name");
        }
    }
}
