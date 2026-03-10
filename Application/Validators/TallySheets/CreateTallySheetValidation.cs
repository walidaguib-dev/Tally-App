using Application.Commands.TallySheets;
using Application.Dtos.TallySheets;
using FluentValidation;

namespace Application.Validators.TallySheets
{
    public class CreateTallySheetDtoValidator : AbstractValidator<CreateTallySheetCommand>
    {
        public CreateTallySheetDtoValidator()
        {


            RuleFor(x => x.dto.TeamsCount)
                .NotEmpty().WithMessage("Teams count is required.")
                .GreaterThan(0).WithMessage("Teams count must be at least 1.")
                .LessThanOrEqualTo(20).WithMessage("Teams count cannot exceed 20.");

            RuleFor(x => x.dto.Shift)
                .IsInEnum().WithMessage("Invalid shift type.");

            RuleFor(x => x.dto.Zone)
                .IsInEnum().WithMessage("Invalid zone type.");

            RuleFor(x => x.dto.ShipId)
                .GreaterThan(0).WithMessage("A valid ship must be selected.");
        }
    }
}