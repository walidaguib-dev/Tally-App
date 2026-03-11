using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Application.Commands.TallySheetTrucks;
using FluentValidation;

namespace Application.Validators.TallySheetTrucks
{
    public class EndTruckSessionTimeValidation : AbstractValidator<EndTruckTimeCommand>
    {
        public EndTruckSessionTimeValidation()
        {
            RuleFor(x => x.Dto.EndTime)
                .NotEmpty()
                .WithMessage("End time is required.")
                .WithName("End Time");
        }
    }
}