using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Application.Commands.TallySheetClients;
using Application.Commands.TallySheetClient;
using Application.Dtos.TallySheetClient;
using FluentValidation;

namespace Application.Validators.TallySheetClients
{
    public class AddMerchandiseValidator : AbstractValidator<CreateTallySheetClientCommand>
    {
        public AddMerchandiseValidator()
        {
            RuleFor(x => x.Dto.TallySheetId)
                .GreaterThan(0)
                .WithMessage("Tally sheet Id must be greater than 0.");

            RuleFor(x => x.Dto.ClientId)
                .GreaterThan(0)
                .WithMessage("Merchandise Id must be greater than 0.");

            RuleFor(x => x.Dto.Quantity)
                .GreaterThanOrEqualTo(0)
                .WithMessage("Quantity cannot be negative.");

            RuleFor(x => x.Dto.Unit)
                .NotEmpty()
                .WithMessage("Unit is required.")
                .Must(x => new[] { "bags", "packages", "pieces", "tons" }.Contains(x.ToLower()))
                .WithMessage("Unit must be: bags, packages, pieces or tons.");
        }
    }
}
