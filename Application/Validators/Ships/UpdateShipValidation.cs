using Application.Dtos.Ships;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Validators.Ships
{
    public class UpdateShipValidation : AbstractValidator<UpdateShipDto>
    {
        public UpdateShipValidation()
        {
            RuleFor(x => x.Name)
                .NotEmpty().WithMessage("Ship name is required.")
                .MaximumLength(100).WithMessage("Ship name cannot exceed 100 characters.");
            RuleFor(x => x.ImoNumber)
                 .NotEmpty().WithMessage("IMO number is required.")
                 .Matches(@"^\d{7}$").WithMessage("IMO number must be exactly 7 digits.");
        }
    }
}
