using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Application.Commands.Teams;
using FluentValidation;

namespace Application.Validators.Teams
{
    public class CreateTeamsValidator : AbstractValidator<CreateTeamCommand>
    {
        public CreateTeamsValidator()
        {
            RuleFor(x => x.Dto.Name).NotEmpty().WithMessage("Team name is required.");
            RuleFor(x => x.Dto.Description)
                .NotEmpty()
                .When(x => x.Dto.Description is not null)
                .WithMessage("Description must be not empty.");
        }
    }
}
