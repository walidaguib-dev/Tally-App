using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Application.Dtos.TallySheets;
using FluentValidation;

namespace Application.Validators.TallySheets
{
    public class UpdatetallySheetValidation : AbstractValidator<UpdateTallySheetDto>
    {
        public UpdatetallySheetValidation()
        {
            RuleFor(x => x.Date)
                .NotEmpty().WithMessage("Date is required.")
                .LessThanOrEqualTo(DateTime.UtcNow).WithMessage("Date cannot be in the future.");

            RuleFor(x => x.TeamsCount)
                .NotEmpty().WithMessage("Teams count is required.")
                .GreaterThan(0).WithMessage("Teams count must be at least 1.")
                .LessThanOrEqualTo(20).WithMessage("Teams count cannot exceed 20.");

            RuleFor(x => x.Shift)
                .IsInEnum().WithMessage("Invalid shift type.");

            RuleFor(x => x.Zone)
                .IsInEnum().WithMessage("Invalid zone type.");

            RuleFor(x => x.ShipId)
                .GreaterThan(0).WithMessage("A valid ship must be selected.");
        }
    }
}