using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using Application.Commands.TallySheetTrucks;
using Application.Dtos.TallySheetTrucks;
using FluentValidation;

namespace Application.Validators.TallySheetTrucks
{
    public class AssignTruckValidation : AbstractValidator<AssignTruckCommand>
    {
        public AssignTruckValidation()
        {
            RuleFor(x => x.Dto.TallySheetId)
                .GreaterThan(0)
                .WithMessage("Id is required")
                .WithName("Tally Sheet Session Id");
            RuleFor(x => x.Dto.TruckId)
                .GreaterThan(0)
                .WithMessage("Id is required")
                .WithName("Truck Id");
            RuleFor(x => x.Dto.StartTime)
                .NotEmpty()
                .NotNull()
                .WithMessage("Start time must be valid.")
                .WithName("Starting Time");
        }
    }
}