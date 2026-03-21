using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Application.Commands.Pauses;
using FluentValidation;

namespace Application.Validators.Pauses
{
    public class EndPauseTimeValidation : AbstractValidator<EndPauseCommand>
    {
        public EndPauseTimeValidation()
        {
            RuleFor(x => x.Dto.EndTime)
                .NotEmpty()
                .NotEmpty()
                .WithMessage("Ending time is required");
        }
    }
}
