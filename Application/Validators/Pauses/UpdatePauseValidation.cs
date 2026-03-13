using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Application.Commands.Pauses;
using FluentValidation;

namespace Application.Validators.Pauses
{
    public class UpdatePauseValidation : AbstractValidator<UpdatePauseCommand>
    {
        public UpdatePauseValidation()
        {
            RuleFor(x => x.Dto.Reason)
                .NotEmpty()
                .WithMessage("Reason is required.").WithName("Pause reason");

            // StartTime: must be a valid time (you can add custom rules if needed)
            RuleFor(x => x.Dto.StartTime)
                .NotNull()
                .WithMessage("Start time is required.").WithName("Pause starting time");

            // Notes: optional, but limit length if provided
            RuleFor(x => x.Dto.Notes)
                .MaximumLength(500)
                .WithMessage("Notes cannot exceed 500 characters.")
                .When(x => !string.IsNullOrEmpty(x.Dto.Notes)).WithName("Pause Notes");

            RuleFor(x => x.Dto.TruckId)
                .GreaterThan(0)
                .When(x => x.Dto.TruckId is not null)
                .WithMessage("Truck ID must be greater than 0 if provided.").WithName("tally sheet truck id");
        }
    }
}