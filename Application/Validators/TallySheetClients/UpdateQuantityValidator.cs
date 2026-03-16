using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Application.Commands.TallySheetClients;
using Application.Commands.TallySheetClient;
using FluentValidation;

namespace Application.Validators.TallySheetClients
{
    public class UpdateQuantityValidator : AbstractValidator<UpdateQuantityCommand>
    {
        public UpdateQuantityValidator()
        {
            RuleFor(x => x.Dto.Quantity)
                .GreaterThanOrEqualTo(0)
                .WithMessage("Quantity cannot be negative.");
        }
    }
}
